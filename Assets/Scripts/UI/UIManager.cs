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
    public CameraController cameraController;

    // UI Buttons
    public GameObject endTurnButton;
    public GameObject attackButton;
    public GameObject moveButton;
    public GameObject undoButton;

    private Vector3 offset = new Vector3(1f, 2f, 0);

    void Start() {
        GameEvents.instance.onTurnEnd += OnTurnEnd;
        GameEvents.instance.onTurnStart += OnTurnStart;
    }

    void Update() {
        if (player != null && player.GetComponent<PlayerMove>().hasMoved) {
            moveButton.SetActive(false);
            undoButton.gameObject.SetActive(true);
        } else {
            undoButton.gameObject.SetActive(false);
        }
    }

    public void EnableTurnActionUI() {
        turnActionPanel.gameObject.SetActive(true);
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

    public void DisplayActionSelect() {
        endTurnButton.gameObject.SetActive(true);
        moveButton.gameObject.SetActive(true);
        attackButton.gameObject.SetActive(true);
    }

    public void DisplayStart() {
        endTurnButton.SetActive(true);
        moveButton.SetActive(false);
        attackButton.SetActive(true);
    }

    public void DisplayAttack() {
        endTurnButton.SetActive(true);
        moveButton.SetActive(true);
        attackButton.SetActive(false);
    }
}
