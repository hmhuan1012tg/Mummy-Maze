using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridMovement : MonoBehaviour
{
    public cellScript current;
    public static Vector3 NotMovable = new Vector3(-1, -1, -1);

    private void Start()
    {
        UpdateCurrentCell();
    }

    public void UpdateCurrentCell()
    {
        Vector3 halfExtents = new Vector3(1f, 1f, 1f);
        Collider[] colliders = Physics.OverlapBox(transform.position + Vector3.down * 3, halfExtents);

        current = colliders[0].GetComponent<cellScript>();
    }

    public Vector3 GetNextPosition(cellScript.Direction direction)
    {
        cellScript next = current.neighborInDirection[direction];
        // there is a cell in this direction, ok
        if (next != null)
        {
            // check if that cell is movable or not
            if (current.neighbors[next])
            {
                // if movable, then move
                // get x and z offset, y unchanged
                float xoffset = next.transform.position.x - transform.position.x;
                float zoffset = next.transform.position.z - transform.position.z;
                Vector3 motion = new Vector3(xoffset, 0.0f, zoffset);
                return transform.position + motion;
            }
            // else stay
        }
        return NotMovable;
    }
}
