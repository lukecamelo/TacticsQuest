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
                break;
            case TurnState.Move:
                Move();
                break;
            case TurnState.Attack:
                // Add attack functions eventually
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
        FindNearestTarget();
        CalculatePath();
        FindSelectableTiles();

        actualTargetTile.target = true;

        yield return new WaitForSeconds(1f);
        turnState = TurnState.Move;
    }

    void CalculatePath() {
        Tile targetTile = GetTargetTile(m_target);
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
        yield return new WaitForSeconds(1f);
        Attack(target);
    }

    public override void Attack(GameObject target) {
        if(turnState == TurnState.Attack) {
            // Call target's take damage method
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            CharacterCombat npcCombat = transform.GetComponent<CharacterCombat>();

            if(npcCombat != null) {
                npcCombat.Attack(targetStats);
            }
        turnState = TurnState.End;
        }
        // targetStats.TakeDamage((int)myStats.attack);
        // hasAttacked = true;
    }
}
