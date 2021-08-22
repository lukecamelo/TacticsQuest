using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenerateTiles : MonoBehaviour
{
    GameObject tilePrefab;

    [MenuItem("Tools/Generate Tiles")]
    public static void GenerateLevelTiles() {
        for (int i = 0; i < 12; i++) {
            GameObject tile = (GameObject)Instantiate(GenerateTiles.GetPrefab("Assets/Prefabs/Tile.prefab"), new Vector3(i, 0, i), Quaternion.identity);
            // tile.GetComponent<Renderer>().material = Resources.Load("Materials/TileMaterial.mat", typeof(Material)) as Material;
        }
    }

    [MenuItem("Tools/Assign Tile Script")]
    public static void AssignTileScript() {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach(GameObject t in tiles) {
            // if(t.GetComponent<Tile> == null)
            t.AddComponent<Tile>();
        }
    }

    [MenuItem("Tools/Assign Highlight Script")]
    public static void AssignHighlightScript() {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject t in tiles) {
            t.AddComponent<HighlightPlus.HighlightEffect>();
            t.AddComponent<HighlightPlus.HighlightTrigger>();
        }
    }

    static GameObject GetPrefab(string location) {
        GameObject prefab = AssetDatabase.LoadAssetAtPath(location, typeof(GameObject)) as GameObject;

        return prefab;
    }
}
