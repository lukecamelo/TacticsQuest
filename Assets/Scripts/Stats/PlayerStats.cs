using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        // Eventual equpment manager stuff
    }

    public override void Die() {
        SceneManager.LoadScene("SampleScene");
    }
}
