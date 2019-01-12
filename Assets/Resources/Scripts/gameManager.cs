using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public playerController playerState;
    public enemyController enemyState;

    struct GameState
    {
        public Vector3 player;
        public Vector3 enemy;
    };

    List<GameState> gameStates = new List<GameState>();
    public int currentState = -1;

    // Start is called before the first frame update
    void Start()
    {
        SaveState();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState.isMoving || enemyState.isMoving)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            // Ctrl + Z
            Undo();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            // Ctrl + R
            Redo();
        }
    }

    public void SaveState()
    {
        gameStates.RemoveRange(currentState + 1, gameStates.Count - 1 - currentState);

        GameState state;
        state.player = playerState.transform.position;
        state.enemy = enemyState.transform.position;
        gameStates.Add(state);

        //Debug.Log(state.player.position);
        currentState++;
    }

    private void Undo()
    {
        if (currentState == 0)
        {
            return;
        }

        currentState--;
        GameState state = gameStates[currentState];

        playerState.transform.position = state.player;
        enemyState.transform.position = state.enemy;

        playerState.movement.UpdateCurrentCell();
        enemyState.movement.UpdateCurrentCell();
    }

    private void Redo()
    {
        if (currentState == gameStates.Count - 1)
        {
            return;
        }

        currentState++;
        GameState state = gameStates[currentState];
        playerState.transform.position = state.player;
        enemyState.transform.position = state.enemy;

        playerState.movement.UpdateCurrentCell();
        enemyState.movement.UpdateCurrentCell();
    }
}
