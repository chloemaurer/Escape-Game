using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator; // animator pour l'animation de transition
    [SerializeField] private string mainSceneName = "Escape Game"; // scène à charger
    [SerializeField] private float transitionTime = 1f; // durée de l'animation en secondes

    // méthode publique pour retourner à la scène principale
    public void ReturnToMainScene()
    {
        StartCoroutine(LoadMainScene());
    }

    private IEnumerator LoadMainScene()
    {
        if (transitionAnimator != null)
            transitionAnimator.SetTrigger("Start"); // lance l'animation

        // attend la fin de l'animation
        yield return new WaitForSeconds(transitionTime);

        // charge la scène principale
        if (!string.IsNullOrEmpty(mainSceneName))
        {
            SceneManager.LoadScene(mainSceneName);
            Debug.Log("Chargement de la scène : " + mainSceneName);
        }
        else
        {
            Debug.LogWarning("Le nom de la scène principale n'est pas défini !");
        }
    }
}
