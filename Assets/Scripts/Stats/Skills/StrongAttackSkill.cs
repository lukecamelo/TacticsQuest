using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StrongAttackSkill : Skill
{
    public float damageMultiplier;

    public override void Activate(GameObject parent) {
        int damage = 0;
        CharacterStats stats = parent.GetComponent<CharacterStats>();
        CharacterCombat characterCombat = parent.GetComponent<CharacterCombat>();

        damage += stats.attack.GetValue() * 4;
        // characterCombat.Attack()
    }
}
