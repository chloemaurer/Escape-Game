using UnityEngine;

public class PorteSymboleAnimation1 : MonoBehaviour
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
        if (PlayerPrefs.GetInt("PorteSymboleOuverte", 0) == 1)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        porteanimation.SetTrigger("ouverte");
        audioSource.clip = doorSound;
        audioSource.Play();

    }
}
