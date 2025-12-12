using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusSelector : MonoBehaviour
{
    [SerializeField] private string statueID; // ID de la statue ("statue1", "statue2", etc.)

    private void Awake()
    {
        // Supprime l'ancienne sélection au démarrage
        PlayerPrefs.DeleteKey("SelectedStatue");
    }

    private void OnMouseDown()
    {
        // Enregistre la statue sélectionnée
        PlayerPrefs.SetString("SelectedStatue", statueID);
        PlayerPrefs.Save();

        Debug.Log("Statue sélectionnée : " + statueID);

        // Charge la scène de rotation des statues
        SceneManager.LoadScene("Statue");
    }
}
