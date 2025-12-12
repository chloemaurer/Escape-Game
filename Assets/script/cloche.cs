using UnityEngine;

public class Bell : MonoBehaviour
{
    [SerializeField] private AudioClip bellSound; // le son joué par cette cloche
    [SerializeField] private int bellID;          // identifiant de la cloche
    private AudioSource audioSource;

    private void Awake()
    {
        // ajoute un AudioSource au besoin et désactive le play automatique
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnMouseDown()
    {
        Ring(); // joue la cloche quand elle est cliquée
    }

    public void Ring()
    {
        if (bellSound == null)
        {
            Debug.LogWarning("Aucun son assigné pour la cloche : " + gameObject.name);
            return;
        }

        // joue le son
        audioSource.clip = bellSound;
        audioSource.Play();

        // notifie le contrôleur de séquence si il existe
        ClocheController checker = ClocheController.Instance;
        if (checker != null)
            checker.RegisterNote(bellID);
    }
}
