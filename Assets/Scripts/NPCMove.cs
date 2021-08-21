using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove
{

    GameObject m_target;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);

        switch(turnState) {
            case TurnState.Waiting:
                break;
            case TurnState.Start:
                // StartCoroutine(PreMoveActions());

                FindNearestTarget();
                CalculatePath();
                FindSelectableTiles();

                actualTargetTile.target = true;
                // Debug.Log("Actual target tile name: " + actualTargetTile.name);
                break;
            case TurnState.Move:
                // StartCoroutine(WaitToMove());
                Move();
                break;
            case TurnState.Attack:
                FindAttackableTiles();
                // StartCoroutine(AttackWithDelay(m_attackTarget));
                Attack(m_attackTarget);
                break;
            case TurnState.End:
                TurnManager.EndTurn();
                break;
            default:
                break;
        }
    }

    // TODO: make this better
    IEnumerator PreMoveActions() {
        yield return new WaitForSeconds(1f);

        FindNearestTarget();
        CalculatePath();
        FindSelectableTiles();

        actualTargetTile.target = true;
        // turnState = TurnState.Move;
    }

    IEnumerator WaitToMove() {
        yield return new WaitForSeconds(.5f);

        Move();
    }

    void CalculatePath() {
        Tile targetTile = GetTargetTile(m_target);
        Debug.Log("original target tile name: " + targetTile);
        FindPath(targetTile);
    }

    void FindNearestTarget() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach(GameObject obj in targets) {
            float d  = Vector3.Distance(transform.position, obj.transform.position);
            
            if (d < distance) {
                distance = d;
                nearest = obj;
            }
        }

        m_attackTarget = nearest;
        m_target = nearest;
    }


    IEnumerator AttackWithDelay(GameObject target) {
        yield return new WaitForSeconds(.4f);
        Attack(target);
    }

    public override void Attack(GameObject target) {
        // Call target's take damage method

        Tile targetTile = GetTargetTile(target);
        // Debug.Log(targetTile.transform.position);

        if (targetTile.attackable && targetTile.distance == 1) {
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            CharacterCombat npcCombat = transform.GetComponent<CharacterCombat>();
            if(npcCombat != null) {
                npcCombat.Attack(targetStats);
            }
        }

        turnState = TurnState.End;
    }

    void FindPath(Tile target) {
        ComputeAdjacencyLists(jumpHeight, target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);
        
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while (openList.Count > 0) {
            // Get the tile with the lowest f value
            Tile t = FindLowestF(openList);

            // add tile to closedList after it has been processed
            closedList.Add(t);

            // if t is the target tile, we exit (success condition)
            if (t == target) {
                Debug.Log(t.transform.position);
                Tile currentTile = GetTargetTile(gameObject);
                // This is where we set variables for attacking the player
                actualTargetTile = FindEndTile(t);

                // If we aren't already there
                MoveToTile(actualTargetTile);
                // if (actualTargetTile == currentTile) {
                //     Debug.Log("Already there!");
                //     turnState = TurnState.Move;
                // } else {
                // }

                return;
            }
            
            // if t is not the target, we look through all of the adjacent tiles
            foreach(Tile tile in t.adjacencyList) {
                if (closedList.Contains(tile)) {
                    // do nothing, tile has already been processed
                } else if (openList.Contains(tile)) {
                    // we have to check if the path through this tile is faster than the path we have already
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    
                    if (tempG < tile.g) {
                        // we have found a faster way
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                } else {
                    // since this tile is on neither list it is the first time we've seen this node
                    // set parent of this tile to t, which is the tile whose adjacencyList we are currently looking through
                    tile.parent = t;

                    // calculate costs for this node
                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.h + tile.g;

                    // Adds tile to openList for potential processing
                    openList.Add(tile);
                }
            }
        }

        // TODO: what to do if there is no path to target tile?
        
    }
}
