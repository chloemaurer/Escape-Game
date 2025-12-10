using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Transform checkpoint = Checkpoint.GetSavedCheckpoint();
        if (checkpoint == null) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        var movement = player.GetComponent<NavigationCharacterControler>();
        var rb = player.GetComponent<Rigidbody>();

        // Désactiver le mouvement avant toute modification physique
        if (movement != null) movement.enabled = false;

        if (rb != null)
        {
            // 1. Réinitialisation complète des forces
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // Empêcher toute influence de la physique pendant la téléportation

            // 2. Téléportation stricte (position et rotation)
            player.transform.position = checkpoint.position;
            player.transform.rotation = checkpoint.rotation;

            // 3. Rétablissement du Rigidbody (délai crucial)
            StartCoroutine(ReenableMovementAfterDelay(movement, rb, 0.05f)); // Délai très court
        }
        else
        {
            // ... (Logique sans Rigidbody inchangée)
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
            // 1. Assurez-vous que la vélocité est ZERO (encore une fois)
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // 2. Rétablir le mode non-Kinematic (la physique reprend le contrôle)
            rb.isKinematic = false;

            // 3. Force le Rigidbody à se réveiller
            rb.WakeUp();
        }

        if (movement != null)
        {
            // Réactiver le contrôle à la fin de la trame de stabilité
            movement.enabled = true;
        }
    }
}