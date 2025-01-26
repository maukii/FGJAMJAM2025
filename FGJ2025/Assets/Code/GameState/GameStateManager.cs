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

    public GameState CurrentGameState { get; private set; }


    void Awake() => Instance = this;

    void Start() => SetGameState(GameState.Playing);

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;
        OnGameStateChanged?.Invoke(CurrentGameState);
    }
}
