using System;
using UnityEngine;

public enum GameState
{
    Menu,
    Playing,
    LevelingUp,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public event Action<GameState> OnGameStateChanged;

    GameState currentState;


    void Awake() => Instance = this;

    void Start() => SetGameState(GameState.Menu);

    public void SetGameState(GameState newState)
    {
        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);
    }
}
