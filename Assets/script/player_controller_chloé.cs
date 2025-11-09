    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;



public class NavigationCharacterControler : MonoBehaviour
{
    [Tooltip("Rigidbody à utiliser pour le mouvement physique (optionnel)")]
    public Rigidbody rb;

    [Tooltip("Vitesse de déplacement en unités/sec")]
    public float Speed = 5f;

    [Tooltip("Vitesse de rotation en degrés/sec")]
    public float RotationSpeed = 180f;

    [Tooltip("Si true utilise Rigidbody.MovePosition/MoveRotation, sinon transform.Translate/Rotate")]
    public bool UsePhysics = true;

    // entrées lues en Update et appliquées en FixedUpdate
    float forwardInput = 0f;
    float turnInput = 0f;
    bool sprintInput = false;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Lire les axes standards Unity (configurés par défaut)
        forwardInput = Input.GetAxis("Vertical");   // W/S, Up/Down
        turnInput = Input.GetAxis("Horizontal");    // A/D, Left/Right
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        // Option : forcer avancer avec Espace
        if (Input.GetKey(KeyCode.Space))
            forwardInput = 1f;
    }

    void FixedUpdate()
    {
        float speedFactor = sprintInput ? 1.5f : 1f;
        float moveDistance = forwardInput * Speed * speedFactor * Time.fixedDeltaTime;
        float turnAngle = turnInput * RotationSpeed * Time.fixedDeltaTime;

        if (UsePhysics && rb != null)
        {
            // Déplacement physique
            Vector3 newPos = rb.position + transform.forward * moveDistance;
            rb.MovePosition(newPos);

            Quaternion deltaRot = Quaternion.Euler(0f, turnAngle, 0f);
            rb.MoveRotation(rb.rotation * deltaRot);
        }
        else
        {
            // Déplacement direct sans physique
            transform.Translate(Vector3.forward * moveDistance, Space.Self);
            transform.Rotate(Vector3.up, turnAngle, Space.Self);
        }
    }
}
