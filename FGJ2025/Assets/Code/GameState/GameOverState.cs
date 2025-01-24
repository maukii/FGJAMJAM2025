using UnityEngine;

public class GameOverState : MonoBehaviour
{
    [SerializeField] Health playerHealth;


    void OnEnable() => playerHealth.OnDeath += GameOver;

    void OnDisable() => playerHealth.OnDeath -= GameOver;

    void GameOver() => GameStateManager.Instance.SetGameState(GameState.GameOver);
}
