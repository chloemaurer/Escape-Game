using UnityEngine;
using UnityEngine.Audio;

public class StatuesFin : MonoBehaviour
{
    [SerializeField] private GameObject dernierRayon; // dernier rayon à détecter
    [SerializeField] private Animator gardenArch; // animation de l'arche du jardin
    [SerializeField] private AudioClip gardenDoor; // son à jouer à l'ouverture

    private AudioSource audioSource;
    private bool triggered = false; // vérifie si l'événement a déjà été déclenché

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (!triggered && dernierRayon.activeInHierarchy)
        {
            triggered = true;
            Debug.Log("Dernier rayon actif, ouverture de l'arche du jardin.");

            if (audioSource != null && gardenDoor != null)
            {
                audioSource.clip = gardenDoor;
                audioSource.Play();
            }

            if (gardenArch != null)
                gardenArch.SetTrigger("openGarden");
        }
    }
}
