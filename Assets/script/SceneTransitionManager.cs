using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneTransitionManager : MonoBehaviour 
{
    public Animator transitionAnimator;
    public string mainSceneName = "Escape Game"; // Nom de la scène principale
    public float transitionTime = 1f; // durée de l'animation

    public void ReturnToMainScene()
    {
        StartCoroutine(LoadMainScene());
    }

    private IEnumerator LoadMainScene()
    {
        // Lance l'animation
        transitionAnimator.SetTrigger("Start");

        // Attend la fin de l’animation
        yield return new WaitForSeconds(transitionTime);

        // Charge la scène principale
        SceneManager.LoadScene(mainSceneName);
    }
}
