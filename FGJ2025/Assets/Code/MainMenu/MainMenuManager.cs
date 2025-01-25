using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioClip MainMenuMusic;
    public GameObject MainViewParent, OptionsParent;

    void Start()
    {
        AudioManager.Instance.PlayMusic(MainMenuMusic);
        ButtonToMainView();
    }

    public void ButtonNewGame()
    {
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(1);
    }

    public void ButtonToMainView()
    {
        OptionsParent.SetActive(false);
        MainViewParent.SetActive(true);
    }

    public void ButtonToOptions()
    {
        MainViewParent.SetActive(false);
        OptionsParent.SetActive(true);
    }
}
