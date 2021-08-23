using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
// using UnityEngine.Experimental.VFX;

public class EnemyStats : CharacterStats
{
    PoofManager poofManager;
    // Start is called before the first frame update
    void Start()
    {
        poofManager = FindObjectOfType<PoofManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die() {
        poofManager.transform.position = transform.position;
        poofManager.PlayEffect();
        Debug.Log(transform.name + " has been killed!");
        TurnManager.RemoveUnit(transform.GetComponent<TacticsMove>());
        Destroy(transform.gameObject);
    }
}
