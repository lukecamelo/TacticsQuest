using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    #region Singleton

    private static GameEvents _instance;

    public static GameEvents instance { get { return _instance; } }


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

    public event Action onTurnEnd;
    public event Action onTurnStart;

    public void TurnEnd() {
        if(onTurnEnd != null) onTurnEnd();
    }

    public void TurnStart() {
        if(onTurnEnd != null) onTurnStart();
    }
}
