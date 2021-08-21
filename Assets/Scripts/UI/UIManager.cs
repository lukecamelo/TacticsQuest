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

    public Image turnActionPanel;
    public Canvas screenUICanvas;
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
            turnActionPanel.transform.position = player.transform.position;
            turnActionPanel.transform.position += offset;
        }

    }

    public void EnableTurnActionUI() {
        turnActionPanel.gameObject.SetActive(true);
        followPlayer = true;

        turnActionPanel.transform.position = player.transform.position;
        turnActionPanel.transform.position += offset;
    }

    public void DisableTurnActionUI() {
        turnActionPanel.gameObject.SetActive(false);
    }

    private void OnTurnEnd() {
        // remove the UI
        DisableTurnActionUI();
    }

    private void OnTurnStart() {
        DisplayCurrentTeam();
        // Get the unit, if its a player, enable the UI and attach it to that player unit
        GameObject activeUnit = TurnManager.activeUnit.gameObject;

        if (activeUnit.tag == "Player") {
            player = activeUnit;
            EnableTurnActionUI();
        }
    }

    private void DisplayCurrentTeam() {
        Text text = screenUICanvas.GetComponentInChildren<Text>();
        text.text = "Active Team: " + TurnManager.turnKey.Peek();
    }
}
