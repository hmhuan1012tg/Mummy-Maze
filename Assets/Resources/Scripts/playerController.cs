using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Rigidbody rb;
    public int speed = 10;
    public gridMovement movement;
    public bool playerTurn = true;
    public bool isMoving = false;
    public Vector3 nextPosition;

    // Update is called once per frame
    private void Update()
    {
        // if not player turn, stay
        if (!playerTurn)
        {
            return;
        }

        // if is moving, not handle input
        if (isMoving)
        {
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveInDirection(cellScript.Direction.Up);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveInDirection(cellScript.Direction.Right);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveInDirection(cellScript.Direction.Down);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveInDirection(cellScript.Direction.Left);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            playerTurn = false;
        }
    }

    private void MoveInDirection(cellScript.Direction direction)
    {
        nextPosition = movement.GetNextPosition(direction);
        // next position is available in this direction
        // so we can move now
        if (!nextPosition.Equals(gridMovement.NotMovable))
        {
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving && !transform.position.Equals(nextPosition))
        {
            rb.velocity = (nextPosition - transform.position) * speed;

            if (Vector3.Distance(nextPosition, transform.position) < 0.1f)
            {
                rb.velocity = Vector3.zero;
                transform.position = nextPosition;
            }
        }
        if (isMoving && transform.position.Equals(nextPosition))
        {
            // finish moving
            isMoving = false;
            movement.UpdateCurrentCell();

            // inform that player has finished moving
            playerTurn = false;
        }
    }
}
