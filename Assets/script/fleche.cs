using UnityEngine;
using UnityEngine.SceneManagement;

public class Fleche : MonoBehaviour
{
    [SerializeField] private string sceneName; // nom de la scène à charger

    private void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            // charge la scène correspondante
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Aucune scène n'est assignée à cette flèche.");
        }
    }
}
