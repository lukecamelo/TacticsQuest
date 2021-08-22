using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public string skillName = "Ability";
    public int turnCooldown = 1;

    // public void Initialize(GameObject obj);
    public virtual void Activate(GameObject parent) {}
}
