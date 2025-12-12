using UnityEngine;

public class puzzleAnimation : MonoBehaviour
{
    [SerializeField] private AudioClip doorSound;   // son joué à l’ouverture du puzzle
    [SerializeField] private Animator puzzle;       // animation du puzzle

    private AudioSource audioSource;                 // source audio pour jouer le son

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        // si le puzzle a déjà été complété, on joue directement l'animation
        if (PlayerPrefs.GetInt("PuzzleCompleted", 0) == 1)
        {
            PuzzleMove();
        }
    }

    // méthode pour lancer l'animation et le son
    public void PuzzleMove()
    {
        if (puzzle != null)
            puzzle.SetTrigger("puzzleopen");

        if (doorSound != null && audioSource != null)
        {
            audioSource.clip = doorSound;
            audioSource.Play();
        }
    }
}
