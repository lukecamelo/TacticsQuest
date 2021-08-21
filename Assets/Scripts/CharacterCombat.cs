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
        int damage = CalculateDamage(targetStats);
        targetStats.TakeDamage(damage);
    }

    private int CalculateDamage(CharacterStats targetStats) {
        int finalDamage = 0;

        finalDamage += myStats.attack.GetValue() + Random.Range(10, 20);

        return finalDamage;
    }
}
