using UnityEngine;

public class tunnelanimation : MonoBehaviour
{
    public AudioClip doorSound;
    private AudioSource audioSource;
    public Animator porteanimation;
    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("tunnelopen", 0) == 1)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        porteanimation.SetTrigger("ouvertureTunnel");
        audioSource.clip = doorSound;
        audioSource.Play();

    }
}
