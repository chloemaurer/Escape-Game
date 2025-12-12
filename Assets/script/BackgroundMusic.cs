using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance; // instance unique de la musique

    [SerializeField] private Slider volumeSlider;   // slider utilisé pour gérer le volume
    [SerializeField] private Toggle musicToggle;    // toggle utilisé pour activer ou couper la musique

    private AudioSource audioSource;

    private void Awake()
    {
        // mise en place du singleton pour garder la musique entre les scènes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.loop = true;

            // lance la musique uniquement si elle n'était pas déjà en cours
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            Debug.LogError("Le GameObject BackgroundMusic n'a pas d'AudioSource.");
        }
    }

    private void Update()
    {
        if (audioSource != null)
        {
            // applique le volume donné par le slider
            if (volumeSlider != null)
                audioSource.volume = Mathf.Clamp01(volumeSlider.value);

            // si le toggle est désactivé, on coupe le son
            if (musicToggle != null)
                audioSource.mute = !musicToggle.isOn;
        }
    }
}
