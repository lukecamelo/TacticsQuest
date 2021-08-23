using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PoofManager : MonoBehaviour
{
    [SerializeField] VisualEffect m_poofEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEffect() {
        m_poofEffect.Play();
    }
}
