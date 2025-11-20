using UnityEngine;

public class Bell : MonoBehaviour
{
    [Header("Assign the sound for this bell")]
    public AudioClip bellSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Ring()
    {
        if (bellSound == null)
        {
            Debug.LogWarning($"Bell '{gameObject.name}' has no sound assigned !");
            return;
        }

        audioSource.clip = bellSound;
        audioSource.Play();

        // Notify the controller
        BellSoundController.Instance.RegisterBellSound(gameObject.name, bellSound.name);
    }
}
