using UnityEngine;
using DG.Tweening;

public class GameOverState : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Canvas gameOverUI;
    [SerializeField] CanvasGroup gameOverCanvasGroup;


    void OnEnable() => playerHealth.OnDeath += GameOver;

    void OnDisable() => playerHealth.OnDeath -= GameOver;

    void GameOver()
    {
        GameStateManager.Instance.SetGameState(GameState.GameOver);

        gameOverCanvasGroup.alpha = 0;
        gameOverUI.gameObject.SetActive(true);
        gameOverCanvasGroup.DOFade(1f, 1f).SetDelay(1f);
    }
}
