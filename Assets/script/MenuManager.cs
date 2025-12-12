using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel; // panel des options

    // lance le jeu depuis le menu
    public void PlayGame()
    {
        // réinitialise les checkpoints et les états du puzzle
        PlayerPrefs.SetString("LastCheckpoint", "Symbole");
        PlayerPrefs.SetInt("labyrintheDoorOpened", 0);
        PlayerPrefs.SetInt("fireset1", 0);
        PlayerPrefs.SetInt("PuzzleCompleted", 0);
        PlayerPrefs.SetInt("PorteSymboleOuverte", 0);

        // charge la scène principale
        SceneManager.LoadScene("Escape Game");
    }

    // quitte le jeu
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Jeu quitté depuis le menu");
    }

    // ouvre le panneau des options
    public void OpenOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(true);
    }

    // ferme le panneau des options
    public void CloseOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }
}
