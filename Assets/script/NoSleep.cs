using UnityEngine;

public class NoSleep : MonoBehaviour
{
    private Rigidbody rb; // référence au Rigidbody de l'objet

    // récupère le Rigidbody au démarrage
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // réveille le Rigidbody à chaque frame pour éviter qu'il se mette en veille
    void Update()
    {
        if (rb != null)
            rb.WakeUp();
    }
}
