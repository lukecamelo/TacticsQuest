using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

    [SerializeField]
    private int m_baseValue;

    private List<int> m_modifiers = new List<int>();

    public int GetValue() {
        int finalValue = m_baseValue;
        m_modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    // Use these when implementing equipment
    public void AddModifier(int modifier) {
        if (modifier != 0)
            m_modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier) {
        if (modifier != 0)
            m_modifiers.Remove(modifier);
    }
}
