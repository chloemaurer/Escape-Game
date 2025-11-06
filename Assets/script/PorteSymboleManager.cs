using UnityEngine;

public class PorteSymboleManager : MonoBehaviour
{
    [Header("Référence des trois parties")]
    public Transform partieExterieure;
    public Transform partieMilieu;
    public Transform partieCentre;

    [Header("Tolérance d’alignement (en degrés)")]
    [SerializeField] private float tolerance = 5f;

    private bool porteOuverte = false;

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
            Debug.Log("🔒 Porte encore fermée...");
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
        // 💥 Ici tu peux mettre ton effet d’ouverture :
        // - Jouer une animation
        // - Activer une porte qui s’ouvre
        // - Changer de scène
        // - Jouer un son

        // Exemple :
        // GetComponent<AudioSource>().Play();
        // Animator.SetTrigger("Open");
    }
}
