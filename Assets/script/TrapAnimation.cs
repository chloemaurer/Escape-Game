using UnityEngine;

public class TrapAnimation : MonoBehaviour
{
    public AudioClip doorSound;
    private AudioSource audioSource;
    public Animator trapanimation;

    private bool doorClosed = false;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("labyrintheDoorOpened", 0) == 1)
        {
            OpenDoor();
        }
        if (PlayerPrefs.GetInt("labyrintheDoorOpened", 0) == 0)
        {
            Debug.Log("Closing door at start.");
            CloseDoor();
        }
        if (PlayerPrefs.GetInt("GearDoorOpened", 0) == 1)
        {
            OpenDoor();
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
