using UnityEngine;
using UnityEngine.SceneManagement; // nécessaire pour LoadScene

public class Fleche : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Aucune scène assignée à la flèche.");
        }
    }
}
