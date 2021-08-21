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

    void Start() {
        GameEvents.instance.onTurnEnd += OnTurnEnd;
        GameEvents.instance.onTurnStart += OnTurnStart;
    }

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
    }

    public void DisableTurnActionUI() {
        panel.gameObject.SetActive(false);
    }

    private void OnTurnEnd() {
        // remove the UI
        DisableTurnActionUI();
    }

    private void OnTurnStart() {
        // Get the unit, if its a player, enable the UI and attach it to that player unit
        GameObject activeUnit = TurnManager.activeUnit.gameObject;

        if (activeUnit.tag == "Player") {
            player = activeUnit;
            EnableTurnActionUI();
        }
    }
}
