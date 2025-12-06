using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    float forwardInput = 0f;
    float turnInput = 0f;
    bool sprintInput = false;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        // IMPORTANT : Détection continue pour éviter de traverser les murs
        if (rb != null)
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKey(KeyCode.Space))
            forwardInput = 1f;
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        float speedFactor = sprintInput ? 1.5f : 1f;
        float moveDistance = forwardInput * Speed * speedFactor * Time.fixedDeltaTime;
        float turnAngle = turnInput * RotationSpeed * Time.fixedDeltaTime;

        if (UsePhysics && !rb.isKinematic)
        {
            // Avancer en respectant la physique (conserver la gravité)
            Vector3 newPos = rb.position + transform.forward * moveDistance;
            rb.MovePosition(newPos);

            // Rotation plus stable
            Quaternion deltaRot = Quaternion.Euler(0f, turnAngle, 0f);
            rb.MoveRotation(rb.rotation * deltaRot);
        }
        else
        {
            transform.Translate(Vector3.forward * moveDistance, Space.Self);
            transform.Rotate(Vector3.up, turnAngle, Space.Self);
        }
    }
}
