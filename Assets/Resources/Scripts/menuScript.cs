using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class buildGrid : MonoBehaviour
{
    [MenuItem("Tools/Assign Cell Material")]
    public static void AssignCellMaterial()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("cell");
        PhysicMaterial material = Resources.Load<PhysicMaterial>("Materials/slippery");

        foreach(var cell in cells)
        {
            cell.GetComponent<BoxCollider>().material = material;
        }
    }

    [MenuItem("Tools/Assign Wall Material")]
    public static void AssignWallMaterial()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        Material material = Resources.Load<Material>("wall");

        foreach (var wall in walls)
        {
            wall.GetComponent<Renderer>().material = material;
        }
    }

    [MenuItem("Tools/Assign Cell Script")]
    public static void AssignCellScript()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("cell");

        foreach (var cell in cells)
        {
            cell.AddComponent<cellScript>();
        }
    }

    [MenuItem("Tools/Count Cells")]
    public static void CountCells()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("cell");
        Debug.Log(cells.Length);
    }
}
