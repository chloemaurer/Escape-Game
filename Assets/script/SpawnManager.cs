using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // garder ce manager entre les scènes
        SceneManager.sceneLoaded += OnSceneLoaded; // s'abonner à l'événement
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // se désabonner proprement
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // récupère le checkpoint sauvegardé
        Transform checkpoint = Checkpoint.GetSavedCheckpoint();
        if (checkpoint == null) return;

        // récupère le joueur
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        var movement = player.GetComponent<NavigationCharacterControler>();
        var rb = player.GetComponent<Rigidbody>();

        // désactive le mouvement pour téléportation
        if (movement != null) movement.enabled = false;

        if (rb != null)
        {
            // réinitialise les vitesses pour éviter les mouvements indésirables
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // bloque la physique

            // téléporte le joueur au checkpoint
            player.transform.position = checkpoint.position;
            player.transform.rotation = checkpoint.rotation;

            // réactive la physique et le mouvement après un très court délai
            StartCoroutine(ReenableMovementAfterDelay(movement, rb, 0.05f));
        }
        else
        {
            // cas sans Rigidbody
            player.transform.position = checkpoint.position;
            player.transform.rotation = checkpoint.rotation;
            if (movement != null) movement.enabled = true;
        }

        Debug.Log("Respawn au checkpoint : " + checkpoint.name);
    }

    private IEnumerator ReenableMovementAfterDelay(NavigationCharacterControler movement, Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false; // relance la physique
            rb.WakeUp(); // assure que le Rigidbody est actif
        }

        if (movement != null)
        {
            movement.enabled = true; // réactive le contrôle du joueur
        }
    }
}
