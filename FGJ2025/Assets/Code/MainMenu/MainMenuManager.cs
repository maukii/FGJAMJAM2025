using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainViewParent, OptionsParent;

    public void ButtonNewGame()
    {
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
