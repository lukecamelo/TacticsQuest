using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public Stat attack;
    public Stat defense;
    // public Stat evasion;

    public GameObject damageTextPrefab;

    void Awake() {
        currentHealth = maxHealth;
    }

    void Update() {
        
    }

    public void TakeDamage(int damage) {
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<DamageText>().Initialize(damage);
        damage -= defense.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        Debug.Log(transform.name + " takes " + damage + " damage!");

        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        Debug.Log(transform.name + " died!");
    }
}
