using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState {
    Start,
    Moving,
    Attack,
    End,
    Waiting,
    ActionSelect,
    Skill
}

public class TacticsMove : MonoBehaviour
{
    List<Tile> selectableTiles = new List<Tile>();
    List<Tile> attackableTiles = new List<Tile>();
    GameObject[] tiles;

    public Stack<Tile> path = new Stack<Tile>();
    public Stack<Tile> highLightPath = new Stack<Tile>();
    public Tile currentTile;

    public int moveRange = 5;
    public int attackRange = 1;
    public float jumpHeight = 1;
    public float moveSpeed = 6;
    public float jumpVelocity = 4.5f;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    protected GameObject m_attackTarget;
    public TurnState turnState = TurnState.Waiting;

    public GameObject activeUnitArrow;

    float halfHeight = 0;

    // Jumping state machine
    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

    // Tile right before NPC's target
    public Tile actualTargetTile;

    protected void Init() {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y;

        // if unit isnt perfectly centered on tile, make it so
        // this avoids doing jumping animations at the beginning of movement 
        CenterOnCurrentTile();

        TurnManager.AddUnit(this);
    }

    public void GetCurrentTile() {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    // Checks below target with raycast to find tile they're standing on
    public Tile GetTargetTile(GameObject target) {
        RaycastHit hit;
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1)) {
            tile = hit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists(float jumpHeight, Tile target) {
        foreach (GameObject tile in tiles) {
            // For every tile, calculate neighbors forward/back and side to side
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(jumpHeight, target);
        }
    }

    public void FindSelectableTiles() {
        ComputeAdjacencyLists(jumpHeight, null);
        // Raycasts to find the tile underneath unit
        GetCurrentTile();

        // Queue represents first in first out list
        Queue<Tile> process = new Queue<Tile>();

        // Adds current tile to front of the queue
        process.Enqueue(currentTile);
        currentTile.visited = true;

        // While the queue still has tiles in it
        while (process.Count > 0) {
            // Removes tile from queue and assigns it to T
            Tile t = process.Dequeue();

            // Adds tile to list of selectable tiles and sets Tile component's selectable flag to true turning it red
            if(!t.attackable) {
                selectableTiles.Add(t);
                t.selectable = true;
            }

            // If the distance to the tile is within units move range
            if (t.distance < moveRange) {
                foreach (Tile tile in t.adjacencyList) {
                    if (!tile.visited) {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;

                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void FindAttackableTiles() {
        ComputeAdjacencyLists(jumpHeight, null);
        GetCurrentTile();
        
        Queue<Tile> process = new Queue<Tile>();

        // Adds current tile to front of the queue
        process.Enqueue(currentTile);
        currentTile.visited = true;

        // While the queue still has tiles in it
        while (process.Count > 0) {
            // Removes tile from queue and assigns it to T
            Tile t = process.Dequeue();

            attackableTiles.Add(t);
            t.attackable = true;

            // If the distance to the tile is within units move range
            if (t.distance < attackRange) {
                foreach (Tile tile in t.attackAdjacencyList) {
                    if (!tile.visited) {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;

                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void HighlightTiles(Tile tile) {
        Debug.Log("hello!!");
        highLightPath.Clear();

        // turnState = TurnState.Move;

        Tile next = tile;

        // Goes backwards from tile we want to move through and adds them to the path
        while (next != null) {
            next.onTheWay = true;
            highLightPath.Push(next);
            next = next.parent;
        }
    }

    public void MoveToTile(Tile tile) {
        // Clears the path stack (last in first out)
        path.Clear();
        tile.target = true;

        // turnState = TurnState.Move;

        Tile next = tile;

        // Goes backwards from tile we want to move through and adds them to the path
        while (next != null) {
            next.onTheWay = true;
            path.Push(next);
            next = next.parent;
        }
    }

    // Removing until i can figure out how the fuck to make it work
    // public void MoveToAttack(Tile tile, GameObject attackTarget) {
    //     m_attackTarget = attackTarget;
    //     Debug.Log("still getting called");
    //     MoveToTile(tile);
    // }

    public void Move() {
        if (path.Count > 0) {
            // Gets the tile at the front of the path;
            Tile t = path.Peek();

            // sets target to that tile's position
            Vector3 target = t.transform.position;

            // Adjust target location so that the unit stands on top of the tile instead of inside it
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f) {

                // if target and unit are not on the same y level do a jump
                bool jump = transform.position.y != target.y;

                if (jump) {
                    Jump(target);
                } else {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }

                // Locomotion
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;

            } else {
                // Tile center reached
                transform.position = target;
                path.Pop();
            }
        } else { // We are out of tiles in the path, aka at the target tile
            RemoveSelectableTiles();

            if(m_attackTarget != null)
                FaceTarget(m_attackTarget.transform);

            turnState = TurnState.Attack;
        }
    }

    // TODO: kinda hacky, find a way around this
    // exists only for making back button work properly
    public void ClearAllTiles() {
        RemoveSelectableTiles();
        RemoveAttackableTiles();
    }

    protected void RemoveSelectableTiles() {

        if (currentTile != null) {
            currentTile.current = false;
            currentTile = null;
        }

        foreach(Tile tile in selectableTiles) {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    protected void RemoveAttackableTiles() {
        if (currentTile != null) {
            currentTile.current = false;
            currentTile = null;
        }

        foreach(Tile tile in attackableTiles) {
            tile.Reset();
        }

        attackableTiles.Clear();
    }

    void CalculateHeading(Vector3 target) {
        heading = target - transform.position;
        heading.Normalize();
    }

    protected void FaceTarget(Transform targetTransform) {
        Debug.Log("Facing target");
        Quaternion targetRotation = Quaternion.LookRotation(targetTransform.position - transform.position);
        Quaternion rotationOnlyY = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // Eventually I'd like a smooth rotation but theres something preventing it from finishing at the moment
        // transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, .5f);
        transform.rotation = rotationOnlyY;
    }

    void SetHorizontalVelocity() {
        velocity = heading * moveSpeed;
    }

    void Jump(Vector3 target) {
        if(fallingDown) {
            FallDownward(target);
        } else if (jumpingUp) {
            JumpUpward(target);
        } else if (movingEdge) {
            MoveToEdge();
        } else {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target) {
        float targetY = target.y;

        // Makes sure character stays upright
        target.y = transform.position.y;

        CalculateHeading(target);

        // if unit is higher than target
        if (transform.position.y > targetY) {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            // halfway point between unit and target aka the edge
            jumpTarget =  transform.position + (target - transform.position) / 2.0f;
        } else { // if unit is lower than target
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }

    void FallDownward(Vector3 target) {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y) {

            // Set all state values to false just in case, as after falling no more jumping logic happens
            fallingDown = false;
            movingEdge = false;
            jumpingUp = false;

            Vector3 p = transform.position;
            p.y = target.y;
            transform.position = p;

            velocity = new Vector3();
        }
    }

    void JumpUpward(Vector3 target) {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y > target.y) {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    void MoveToEdge() {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f) {
            SetHorizontalVelocity();
        } else {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5.0f;
            velocity.y = 1.5f;
        }
    }

    public void BeginTurn() {
        activeUnitArrow.SetActive(true);
        GameEvents.instance.TurnStart();
        turnState = TurnState.ActionSelect;
    }

    public void EndTurn() {
        activeUnitArrow.SetActive(false);
        GameEvents.instance.TurnEnd();
        turnState = TurnState.Waiting;
    }

    public virtual void Attack(GameObject target) {
        // nothing
    }

    public virtual void UseSkill(GameObject target, string skillName) {
        // nothing for now
    }

    protected Tile FindLowestF(List<Tile> list) {
        Tile lowest = list[0];

        foreach(Tile t in list) {
            if (t.f < lowest.f) {
                lowest = t;
            }
        }

        list.Remove(lowest);

        return lowest;
    }

    // Gets the tile right before the target
    protected Tile FindEndTile(Tile t) {
        Stack<Tile> tempPath = new Stack<Tile>();

        Tile next = t.parent;

        while (next != null) {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= moveRange) {
            return t.parent;
        }

        Tile endTile = null;

        for (int i = 0; i <= moveRange; i++) {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

    public void CenterOnCurrentTile() {
        Tile t = GetTargetTile(gameObject);
        Vector3 target = t.transform.position;

        target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

        if (Vector3.Distance(transform.position, target) >= 0.05f) {
            transform.position = target;
        }
    }
}
