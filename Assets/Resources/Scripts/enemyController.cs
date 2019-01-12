using System;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public playerController player;
    public Rigidbody rb;
    public gridMovement movement;
    public Vector3 nextPosition;
    public bool isMoving;
    public int moveCount = 0;
    public int speed = 10;
    public gameManager manager;

    // Update is called once per frame
    void Update()
    {
        if (player.playerTurn)
        {
            return;
        }

        if (isMoving)
        {
            return;
        }

        ChasePlayer();
    }

    private void ChasePlayer()
    {
        cellScript.Direction direction = GetDirection();

        if (direction.Equals(cellScript.Direction.None))
        {
            moveCount++;
            if (moveCount == 2)
            {
                moveCount = 0;
                player.playerTurn = true;
                manager.SaveState();
            }
        }
        else
        {
            nextPosition = movement.GetNextPosition(direction);
            isMoving = true;
        }
    }

    private cellScript.Direction GetDirection()
    {
        Vector3 curPos = transform.position;
        Vector3 playerPos = player.transform.position;
        float currentDistance = MahattanDistance(curPos, playerPos);
        cellScript.Direction direction = cellScript.Direction.None;

        // try to move horizontally
        if (MahattanDistance(curPos + Vector3.right * 3, playerPos) < currentDistance
            && !movement.GetNextPosition(cellScript.Direction.Right).Equals(gridMovement.NotMovable))
        {
            direction = cellScript.Direction.Right;
        }
        else if (MahattanDistance(curPos - Vector3.right * 3, playerPos) < currentDistance
            && !movement.GetNextPosition(cellScript.Direction.Left).Equals(gridMovement.NotMovable))
        {
            direction = cellScript.Direction.Left;
        }
        // try to move vertically
        else if (MahattanDistance(curPos + Vector3.forward * 3, playerPos) < currentDistance
            && !movement.GetNextPosition(cellScript.Direction.Up).Equals(gridMovement.NotMovable))
        {
            direction = cellScript.Direction.Up;
        }
        else if (MahattanDistance(curPos - Vector3.forward * 3, playerPos) < currentDistance
            && !movement.GetNextPosition(cellScript.Direction.Down).Equals(gridMovement.NotMovable))
        {
            direction = cellScript.Direction.Down;
        }

        return direction;
    }

    private float MahattanDistance(Vector3 start, Vector3 end)
    {
        return Math.Abs(start.x - end.x) + Math.Abs(start.y - end.y) + Math.Abs(start.z - end.z);
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

            moveCount++;
            if (moveCount == 2)
            {
                player.playerTurn = true;
                moveCount = 0;
                manager.SaveState();
            }
        }
    }
}
