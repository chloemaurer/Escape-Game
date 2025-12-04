using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusSelector : MonoBehaviour
{
    public string statueID; // Exemple : "Statue1"

    private void OnMouseDown()
    {
        PlayerPrefs.SetString("SelectedStatus", statueID);
        PlayerPrefs.Save();

        SceneManager.LoadScene("SceneRotation");
    }
}


