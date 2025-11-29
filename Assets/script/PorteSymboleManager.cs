using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PorteSymboleManager : MonoBehaviour
{
    [Header("Référence des trois parties")]
    public Transform partieExterieure;
    public Transform partieMilieu;
    public Transform partieCentre;
    public AudioClip doorSound;
    private AudioSource audioSource;
    private PorteSymboleAnimation1 porteAnimation;


    [Header("Tolérance d’alignement (en degrés)")]
    [SerializeField] private float tolerance = 5f;

    private bool porteOuverte = false;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        PlayerPrefs.SetInt("PorteSymboleOuverte", 0);
    }

    public void CheckAlignment()
    {
        Debug.Log("🔍 Vérification de l’alignement des parties...");
        float angleExt = NormalizeAngle(partieExterieure.localEulerAngles.z);
        float angleMil = NormalizeAngle(partieMilieu.localEulerAngles.z);
        float angleCen = NormalizeAngle(partieCentre.localEulerAngles.z);

        // Vérifie si les 3 angles sont "proches"
        if (Mathf.Abs(angleExt - angleMil) < tolerance &&
            Mathf.Abs(angleMil - angleCen) < tolerance)
        {
            porteOuverte = true;
            Debug.Log("✅ Porte ouverte !");
            
            OnPorteOuverte();
        }
        else
        {
            Debug.Log("🔒 Porte encore fermée");

        }
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }

    private void OnPorteOuverte()
    {
        PlayerPrefs.SetInt("PorteSymboleOuverte", 1);
        SceneManager.LoadScene("Escape Game");

    }
}
