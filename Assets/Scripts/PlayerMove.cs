using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : TacticsMove
{
    public Transform m_tacticsCamera;
    private Camera[] m_cameras;
    private Camera m_currentCamera;
    private CharacterStats m_myStats;

    public Tile startingTile;
    public bool hasMoved = false;

    public Skill[] skills;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        m_tacticsCamera = FindObjectOfType<TacticsCamera>().transform;
        m_myStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        // HighlightPath();
        switch(turnState) {
            case TurnState.Waiting:
                break;

            case TurnState.ActionSelect:
                startingTile = GetTargetTile(gameObject);
                hasMoved = false;
                UIManager.instance.DisplayActionSelect();
                break;

            case TurnState.Start:
                UIManager.instance.DisplayStart();
                // UIManager.instance.EnableTurnActionUI();
                FindSelectableTiles();
                // CheckMouse();
                GenericCheckMouse("move");
                // possibly need a function for moving unit to tile before enemy if clicking on enemy
                break;

            case TurnState.Moving:
                UIManager.instance.DisableTurnActionUI();
                Move();
                hasMoved = true;
                break;

            case TurnState.Attack:
                UIManager.instance.EnableTurnActionUI();
                UIManager.instance.DisplayAttack();
                // Attack functions go here
                FindAttackableTiles();
                // CheckMouseForAttack();
                GenericCheckMouse("attack");
                break;

            case TurnState.Skill:
                FindAttackableTiles();
                // CheckMouseForSkill();
                GenericCheckMouse("skill");
                break;

            case TurnState.End:
                // UIManager.instance.DisableTurnActionUI();
                TurnManager.EndTurn();
                break;

            default:
                break;
        }
    }

    void HighlightPath() {
        SetCurrentCamera();

        Ray ray = m_currentCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag == "Tile") {
                Tile t = hit.collider.GetComponent<Tile>();

                if (t.selectable) {
                    HighlightTiles(t);
                }
            }
        }
    }

    void GenericCheckMouse(string action) {
        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIObject()) {
            SetCurrentCamera();

            Ray ray = m_currentCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                switch (action) {

                    case "move":
                        if (hit.collider.tag == "Tile") {
                            Tile t = hit.collider.GetComponent<Tile>();

                            if (t.selectable) {
                                // move here
                                MoveToTile(t);
                                turnState = TurnState.Moving;
                            }
                        }
                        break;

                    case "attack":
                        if (hit.collider.tag == "NPC") {
                            GameObject t = hit.collider.gameObject;

                            // Get their current tile
                            Tile targetTile = GetTargetTile(t);

                            // If we are next to enemy
                            if (targetTile.attackable && targetTile.distance == 1) {
                                Debug.Log("Attack");
                                Attack(t);
                                RemoveAttackableTiles();

                                turnState = TurnState.End;
                            }
                        }
                        break;

                    case "skill":
                        if (hit.collider.tag == "NPC") {
                            GameObject t = hit.collider.gameObject;

                            // Get their current tile
                            Tile targetTile = GetTargetTile(t);

                            // If we are next to enemy
                            if (targetTile.attackable && targetTile.distance == 1) {
                                Debug.Log("Skill");
                                // Attack(t);
                                UseSkill(t, "Strong Attack");
                                // Skill logic goes here
                                RemoveAttackableTiles();

                                turnState = TurnState.End;
                            }
                        }
                        break;

                    default:
                        break;
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
        base.Attack(target);
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        CharacterCombat playerCombat = transform.GetComponent<CharacterCombat>();

        if(playerCombat != null) {
            playerCombat.Attack(targetStats);
        }
    }

    public override void UseSkill(GameObject target, string skillName) {
        base.UseSkill(target, skillName);

        // Debug.Log(skills[0].skillName);
        foreach(Skill skill in skills) {
            if (skill.skillName == skillName && m_myStats.currentMp > skill.mpCost) {
                skill.Activate(gameObject, target);
                m_myStats.ReduceMp(skill.mpCost);
            }
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
