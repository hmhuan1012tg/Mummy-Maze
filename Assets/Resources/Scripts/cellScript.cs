using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellScript : MonoBehaviour
{
    public enum Direction
    {
        Up, Right, Down, Left, None
    };

    public Dictionary<cellScript, bool> neighbors = new Dictionary<cellScript, bool>();
    public Dictionary<Direction, cellScript> neighborInDirection = new Dictionary<Direction, cellScript>();

    private void Start()
    {
        FindNeighbors();
    }

    private void FindNeighbors()
    {
        neighborInDirection.Add(Direction.Up, CheckCell(Vector3.forward));
        neighborInDirection.Add(Direction.Down, CheckCell(-Vector3.forward));
        neighborInDirection.Add(Direction.Right, CheckCell(Vector3.right));
        neighborInDirection.Add(Direction.Left, CheckCell(-Vector3.right));
    }

    private cellScript CheckCell(Vector3 direction)
    {
        Vector3 halfExtents = new Vector3(1f, 1f, 1f);
        Vector3 wallDetectionBox = new Vector3(0.25f, 0.25f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction * 3, halfExtents);

        foreach (var collider in colliders)
        {
            cellScript cell = collider.GetComponent<cellScript>();
            if (cell != null)
            {
                Vector3 middle = (transform.position + cell.transform.position) / 2;
                if (Physics.OverlapBox(middle + Vector3.up * 2, wallDetectionBox).Length > 0)
                {
                    neighbors.Add(cell, false);
                }
                else
                {
                    neighbors.Add(cell, true);
                }
            }

            return cell;
        }

        return null;
    }
}
