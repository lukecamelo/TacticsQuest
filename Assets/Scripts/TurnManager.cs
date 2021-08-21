using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // {"NPC": [NPC1TacticsMove, NPC2TacticsMove], "Player: [PlayerTacticsMove, etc]}
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
    // A Queue containg the strings of the names of both teams
    static Queue<string> turnKey = new Queue<string>();
    // The units making up the team whose turn it is
    public static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();
    public static TacticsMove activeUnit;

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0) {
            InitTeamTurnQueue();
        }
    }

    static void InitTeamTurnQueue() {
        List<TacticsMove> teamList = units[turnKey.Peek()];

        foreach(TacticsMove unit in teamList) {
            turnTeam.Enqueue(unit);
        }

        StartTurn();
    }

    public static void StartTurn() {
        if (turnTeam.Count > 0) {
            activeUnit = turnTeam.Peek();
            turnTeam.Peek().BeginTurn();
        } else {
            turnKey.Dequeue();
        }
    }

    public static void EndTurn() {
        TacticsMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0) {
            StartTurn();
        } else {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(TacticsMove unit) {
        List<TacticsMove> list;

        if (!units.ContainsKey(unit.tag)) {
            list = new List<TacticsMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag)) {
                turnKey.Enqueue(unit.tag);
            }
        } else {
            list = units[unit.tag];
        }

        list.Add(unit);
    }

    public static void SkipUnitMove() {
        activeUnit.turnState = TurnState.Attack;
    }

    public static void RemoveUnit(TacticsMove unit) {
        units[unit.tag].Remove(unit);

        // Maybe determine if that was the last unit on a team, meaning the battle is over
        // Whether that be a player victory or an NPC victory
    }
}
