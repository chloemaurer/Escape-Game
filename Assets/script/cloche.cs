using UnityEngine;

public class Bell : MonoBehaviour 
{
    [Header("Assign the sound for this bell")]
    public AudioClip bellSound;
    public Camera playercamera;
    public int bellID;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // clic gauche
        {
            Ray ray = playercamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Bell bell = hit.collider.GetComponent<Bell>();
                if (bell != null)
                    bell.Ring();
            }
        }
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
        ClocheController checker = FindObjectOfType<ClocheController>();
        if (checker != null)
            checker.RegisterNote(bellID);

    }


}
