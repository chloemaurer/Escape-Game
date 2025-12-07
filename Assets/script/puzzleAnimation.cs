using UnityEngine;

public class puzzleAnimation : MonoBehaviour
{
    public AudioClip doorSound;
    private AudioSource audioSource;
    public Animator puzzle;
    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("PuzzleCompleted", 0) == 1)
        {
            PuzzleMove();
        }
    }

    public void PuzzleMove()
    {
        puzzle.SetTrigger("puzzleopen");
        audioSource.clip = doorSound;
        audioSource.Play();

    }
}
