using UnityEngine;
using UnityEngine.SceneManagement;

public class PorteSymboleManager : MonoBehaviour
{
    [SerializeField] private Transform partieExterieure;   // partie extérieure de la porte
    [SerializeField] private Transform partieMilieu;       // partie du milieu
    [SerializeField] private Transform partieCentre;       // partie centrale
    [SerializeField] private AudioClip doorSound;          // son joué à l’ouverture
    [SerializeField] private float tolerance = 5f;         // tolérance d’alignement en degrés

    private AudioSource audioSource;                        // source audio pour jouer le son
    private bool porteOuverte = false;                     // indique si la porte est ouverte

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // réinitialisation de l’état de la porte
        PlayerPrefs.SetInt("PorteSymboleOuverte", 0);
    }

    // vérifie si les trois parties sont correctement alignées
    public void CheckAlignment()
    {
        Debug.Log("Vérification de l’alignement des parties...");

        float angleExt = NormalizeAngle(partieExterieure.localEulerAngles.z);
        float angleMil = NormalizeAngle(partieMilieu.localEulerAngles.z);
        float angleCen = NormalizeAngle(partieCentre.localEulerAngles.z);

        // si tous les angles sont proches → porte ouverte
        if (Mathf.Abs(angleExt - angleMil) < tolerance &&
            Mathf.Abs(angleMil - angleCen) < tolerance)
        {
            porteOuverte = true;
            Debug.Log("Porte ouverte !");
            OnPorteOuverte();
        }
        else
        {
            Debug.Log("Porte encore fermée");
        }
    }

    // normalise un angle entre 0 et 360
    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }

    // actions à effectuer lorsque la porte est ouverte
    private void OnPorteOuverte()
    {
        PlayerPrefs.SetInt("PorteSymboleOuverte", 1);

        // jouer le son si disponible
        if (doorSound != null && audioSource != null)
        {
            audioSource.clip = doorSound;
            audioSource.Play();
        }

        // passer à la scène suivante
        SceneManager.LoadScene("Escape Game");
    }
}
