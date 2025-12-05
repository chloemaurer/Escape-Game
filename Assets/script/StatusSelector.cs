using UnityEngine;


public class StatusSelector : MonoBehaviour
{
    public string statueID; // Doit être "statue1", "statue2", etc.

    public void Awake()
    {
        PlayerPrefs.DeleteKey("SelectedStatue");
    }
    private void OnMouseDown()
    {
        PlayerPrefs.SetString("SelectedStatue", statueID);
        PlayerPrefs.Save();

        Debug.Log("Statue sélectionnée : " + statueID);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Statue");
    }
}
