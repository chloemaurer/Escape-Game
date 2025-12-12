using UnityEngine;

public class TrapDetection : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private TrapAnimation trap;   // La trappe à fermer
    [SerializeField] private AudioClip trapSound;  // Son à jouer à l'activation

    [Header("Paramètres")]
    [SerializeField] private string playerTag = "Player";

    private AudioSource audioSource;
    private bool hasTriggered = false;

    private void Awake()
    {
        // Crée un AudioSource si nécessaire
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrée détectée : " + other.name);

        if (hasTriggered) return;

        if (other.CompareTag(playerTag))
        {
            hasTriggered = true;

            // Jouer le son de déclenchement
            if (trapSound != null)
            {
                audioSource.clip = trapSound;
                audioSource.Play();
            }

            // Fermer la trappe
            if (trap != null)
            {
                trap.CloseDoor();
            }

            // Désactiver le collider pour éviter des triggers répétés
            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }
    }
}
