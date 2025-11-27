using UnityEngine;

public class Bell : MonoBehaviour
{
    [Header("Assign the sound for this bell")]
    public AudioClip bellSound;
    private AudioSource audioSource;
    public int bellID;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnMouseDown()
    {
        Ring();
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

        // Notifier le contrôleur de séquence
        ClocheController checker = ClocheController.Instance;
        if (checker != null)
            checker.RegisterNote(bellID);
    }
}
