using UnityEngine;

public class PorteSymboleAnimation1 : MonoBehaviour
{
    [SerializeField] private AudioClip doorSound;       // son joué à l’ouverture
    [SerializeField] private Animator porteanimation;   // animation de la porte

    private AudioSource audioSource;                     // source audio pour jouer le son

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        // si la porte a déjà été ouverte précédemment, on l’ouvre directement
        if (PlayerPrefs.GetInt("PorteSymboleOuverte", 0) == 1)
        {
            OpenDoor();
        }
    }

    // méthode pour ouvrir la porte
    public void OpenDoor()
    {
        if (porteanimation != null)
            porteanimation.SetTrigger("ouverte");

        if (doorSound != null)
        {
            audioSource.clip = doorSound;
            audioSource.Play();
        }
    }
}
