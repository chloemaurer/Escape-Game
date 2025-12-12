using UnityEngine;

public class TrapAnimation : MonoBehaviour
{
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private Animator trapanimation;

    private AudioSource audioSource;
    private bool doorClosed = false;

    private void Awake()
    {
        // Ajoute un AudioSource pour jouer les sons
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        // Si le labyrinthe est déjà ouvert, on ouvre la trappe
        if (PlayerPrefs.GetInt("labyrintheDoorOpened", 0) == 1)
        {
            OpenDoor();
        }
        else
        {
            // Sinon, on ferme la trappe au démarrage
            Debug.Log("Fermeture de la trappe au démarrage.");
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        trapanimation.SetTrigger("trapouverte");
        audioSource.clip = doorSound;
        audioSource.Play();
    }

    public void CloseDoor()
    {
        if (doorClosed) return;

        trapanimation.SetTrigger("trapfermee");
        audioSource.clip = doorSound;
        audioSource.Play();

        doorClosed = true;
    }
}
