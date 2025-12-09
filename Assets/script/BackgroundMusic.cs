using UnityEngine;
using UnityEngine.UI; // nécessaire pour Slider et Toggle

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance; // Singleton

    [Header("UI Controls")]
    public Slider volumeSlider;   // le slider du menu
    public Toggle musicToggle;    // le toggle du menu

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton
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
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource manquant sur le GameObject BackgroundMusic !");
        }
    }

    private void Update()
    {
        if (audioSource != null)
        {
            // récupère la valeur du slider et applique le volume
            if (volumeSlider != null)
                audioSource.volume = Mathf.Clamp01(volumeSlider.value);

            // récupère la valeur du toggle et applique le mute
            if (musicToggle != null)
                audioSource.mute = !musicToggle.isOn;
        }
    }
}
