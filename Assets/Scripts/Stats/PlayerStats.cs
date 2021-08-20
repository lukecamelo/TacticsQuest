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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(10);
        }
    }

    public override void Die() {
        SceneManager.LoadScene("SampleScene");
    }
}
