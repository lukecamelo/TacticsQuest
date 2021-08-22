using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StrongAttackSkill : Skill
{
    public int damageMultiplier;

    public override void Activate(GameObject parent, GameObject target) {
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        CharacterStats myStats = parent.GetComponent<CharacterStats>();

        int damage = myStats.attack.GetValue() + Random.Range(10, 20) * damageMultiplier;

        targetStats.TakeDamage(damage);
    }
}
