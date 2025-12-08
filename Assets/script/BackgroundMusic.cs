using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance; // Singleton pour éviter les duplications

    [Header("Clip de musique de fond")]
    public AudioClip backgroundClip;

    [Header("Volume de la musique")]
    [Range(0f, 1f)]
    public float volume = 0.5f;

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton : si une instance existe déjà, détruire ce GameObject
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ne pas détruire entre les scènes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ajouter AudioSource si inexistant
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundClip;
        audioSource.loop = true;           // Boucle infinie
        audioSource.spatialBlend = 0f;     // 2D, volume constant partout
        audioSource.volume = volume;
        audioSource.playOnAwake = false;

        // Jouer la musique
        if (backgroundClip != null)
            audioSource.Play();
    }
}
