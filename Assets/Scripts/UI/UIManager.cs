using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region Singleton

    private static UIManager _instance;

    public static UIManager instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    #endregion

    public Image panel;
    public GameObject player;
    private Vector3 offset = new Vector3(1f, 2f, 0);
    private bool followPlayer = false;
    // Responsibilites
    
    // Create UI elements
    // - Turn actions (Wait/End Turn, Attack, Move)

    // Enable UI elements
    // Disable UI elements
    // Set relative UI position

    void Update() {
        if(followPlayer) {
            panel.transform.position = player.transform.position;
            panel.transform.position += offset;
        }
    }

    public void EnableTurnActionUI() {
        panel.gameObject.SetActive(true);
        followPlayer = true;

        panel.transform.position = player.transform.position;
        panel.transform.position += offset;
        // panel.transform.SetParent(player.transform);
    }

    public void DisableTurnActionUI() {
        panel.gameObject.SetActive(false);
    }
}
