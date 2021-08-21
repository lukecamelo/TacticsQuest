using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : TacticsMove
{
    public Transform m_tacticsCamera;
    private Camera[] m_cameras;
    private Camera m_currentCamera;
    private Animator animator;
    // private GameObject m_attackTarget;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        m_tacticsCamera = FindObjectOfType<TacticsCamera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);

        switch(turnState) {
            case TurnState.Waiting:
                break;
            case TurnState.Start:
                // UIManager.instance.EnableTurnActionUI();
                FindSelectableTiles();
                CheckMouse();
                // possibly need a function for moving unit to tile before enemy if clicking on enemy
                break;
            case TurnState.Move:
                Move();
                break;
            case TurnState.Attack:
                // Attack functions go here
                FindAttackableTiles();
                CheckMouseForAttack();
                break;
            case TurnState.End:
                // UIManager.instance.DisableTurnActionUI();
                TurnManager.EndTurn();
                break;
            default:
                break;
        }
    }

    void CheckMouse() {
        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIObject()) {
            SetCurrentCamera();

            Ray ray = m_currentCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "Tile") {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable) {
                        // move here
                        MoveToTile(t);
                        turnState = TurnState.Move;
                    }
                }
            }
        }
    }

    void CheckMouseForAttack() {
        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIObject()) {
            SetCurrentCamera();

            Ray ray = m_currentCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "NPC" && transform.tag != "NPC") {
                    // Get NPC gameObject
                    GameObject t = hit.collider.gameObject;
                    // m_attackTarget = t;

                    // Get their current tile
                    Tile targetTile = GetTargetTile(t);
                    // Tile endTile = FindEndTile(targetTile);

                    // If we are next to enemy
                    if (targetTile.attackable && targetTile.distance == 1) {
                        Debug.Log("Attack");
                        Attack(t);
                        RemoveAttackableTiles();

                        turnState = TurnState.End;
                    }

                    // Check if that tile is within our range
                    if (targetTile.distance != 0 && targetTile.distance >= 1) {
                        // MoveToAttack(endTile, m_attackTarget);
                    }

                    // Debug.Log(targetTile.distance);

                }
            }
        }
    }

    void SetCurrentCamera() {
        m_cameras = m_tacticsCamera.GetComponentsInChildren<Camera>();
        foreach(Camera cam in m_cameras) {
            if(cam.isActiveAndEnabled) {
                m_currentCamera = cam;
            }
        }
    }

    public override void Attack(GameObject target) {
        // Call target's take damage method
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        CharacterCombat playerCombat = transform.GetComponent<CharacterCombat>();

        if(playerCombat != null) {
            playerCombat.Attack(targetStats);
        }
    }

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
