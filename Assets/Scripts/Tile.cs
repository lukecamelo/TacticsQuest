using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public bool attackable = false;
    public bool onTheWay = false;

    public List<Tile> adjacencyList = new List<Tile>();
    public List<Tile> attackAdjacencyList = new List<Tile>();

    // BFS variables
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    // A* variables

    // g + h
    public float f = 0;
    // cost from parent to current tile
    public float g = 0;
    // cost from processed tile to destination
    public float h = 0;

    // Update is called once per frame
    void Update()
    {
        if (current) {
            GetComponent<Renderer>().material.color = Color.magenta;
        } else if (target) {
            GetComponent<Renderer>().material.color = Color.red;
        } else if (onTheWay) {
            GetComponent<Renderer>().material.color = Color.yellow;
        } else if (selectable) {
            GetComponent<Renderer>().material.color = Color.green;
        } else if (attackable) {
            GetComponent<Renderer>().material.color = Color.cyan;
        } else {
            GetComponent<Renderer>().material.color = Color.white;
        }

        if (selectable) {
            GetComponent<HighlightPlus.HighlightEffect>().enabled = true;
            GetComponent<HighlightPlus.HighlightTrigger>().enabled = true;
        }  else {
            GetComponent<HighlightPlus.HighlightEffect>().enabled = false;
            GetComponent<HighlightPlus.HighlightTrigger>().enabled = false;
        }
    }

    public void Reset() {
        adjacencyList.Clear();
        attackAdjacencyList.Clear();

        walkable = true;
        current = false;
        target = false;
        selectable = false;
        attackable = false;
        onTheWay = false;

        visited = false;
        parent = null;
        distance = 0;

        f = g = h = 0;
    }

    public void FindNeighbors(float jumpHeight, Tile target) {
        Reset();

        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target) {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders) {
            Tile tile = item.GetComponent<Tile>();

            if (tile != null && tile.walkable) {
                RaycastHit hit;

                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target)) {
                    adjacencyList.Add(tile);
                } else if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1)) {
                    // TODO: change the way tiles are added to attackAdjacencyList to allow for attacks for more than one tile away
                    if (hit.collider.tag == "NPC" || hit.collider.tag == "Player") {
                        attackAdjacencyList.Add(tile);
                    }
                }
            }
        }
    }
}
