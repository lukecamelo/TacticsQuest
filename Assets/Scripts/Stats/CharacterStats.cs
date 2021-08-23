using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth { get; private set; }
    public int maxMp = 50;
    public int currentMp { get; private set; }
    public Stat attack;
    public Stat defense;
    
    public Slider healthBar;

    public GameObject damageTextPrefab;

    void Awake() {
        currentHealth = maxHealth;
        currentMp = maxMp;

        healthBar = GetComponentInChildren<Slider>();
        healthBar.value = CalculateHealth();
    }

    void Update() {
        
    }

    public void TakeDamage(int damage) {
        Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<DamageText>().Initialize(damage);
        damage -= defense.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        healthBar.value = CalculateHealth();
        
        Debug.Log(transform.name + " takes " + damage + " damage!");

        if (currentHealth <= 0) {
            Die();
        }
    }

    public void ReduceMp(int mpCost) {
        currentMp -= mpCost;

        if (currentMp < 0) {
            currentMp = 0;
        }
    }

    public virtual void Die() {
        Debug.Log(transform.name + " died!");
    }

    float CalculateHealth() {
        Debug.Log("Calculating health for: " + transform.name);
        return currentHealth / maxHealth;
    }
}
