using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    public CanvasGroup buttonLabelsGroup;
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

        HideButtonLabels();
    }

    public void ButtonToMainView()
    {
        OptionsParent.SetActive(false);
        MainViewParent.SetActive(true);

        ShowButtonLabels(); 
    }

    public void ButtonToOptions()
    {
        MainViewParent.SetActive(false);
        OptionsParent.SetActive(true);

        HideButtonLabels();
    }

    public void ButtonToQuit()
    {
        Application.Quit();
    }

    void HideButtonLabels()
    {
        buttonLabelsGroup.DOFade(0, .25f);
    }

    void ShowButtonLabels()
    {
        buttonLabelsGroup.alpha = 1f;
    }
}
