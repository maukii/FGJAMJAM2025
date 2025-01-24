using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas menuCanvas;
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas gameOverCanvas;


    void OnEnable() => GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;

    void OnDisable() => GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;

    void OnGameStateChanged(GameState state)
    {
        DisableUIs();

        switch (state)
        {
            case GameState.Menu:
                menuCanvas.gameObject.SetActive(true);
                break;
            case GameState.Playing:
                gameCanvas.gameObject.SetActive(true);
                break;
            case GameState.GameOver:
                gameOverCanvas.gameObject.SetActive(true);
                break;
        }
    }

    void DisableUIs()
    {
        menuCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
    }
}
