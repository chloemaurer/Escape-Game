using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panel Options")]
    public GameObject optionsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Escape Game");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitter le jeu");
    }

    public void OpenOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }
}
