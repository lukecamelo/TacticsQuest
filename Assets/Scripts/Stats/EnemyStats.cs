using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die() {
        Debug.Log(transform.name + " has been killed!");
        TurnManager.RemoveUnit(transform.GetComponent<TacticsMove>());
        Destroy(transform.gameObject);
    }
}
