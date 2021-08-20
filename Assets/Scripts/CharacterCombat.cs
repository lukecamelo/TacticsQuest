using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;

    void Start() {
        myStats = GetComponent<CharacterStats>();
    }

    public void Attack(CharacterStats targetStats) {
        targetStats.TakeDamage(myStats.attack.GetValue());
    }
}
