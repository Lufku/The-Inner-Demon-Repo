using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject controlsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ShowControls()
    {
        controlsPanel.SetActive(true);
    }

    public void HideControls()
    {
        controlsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("EXIT GAME");
    }

    public void Ready1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Ready2()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
