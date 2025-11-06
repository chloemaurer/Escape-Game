using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Tooltip("Fraction of the screen that"), Range(0, 1)]
    public float ScreenInteractionFraction = 0.1f;

    public GameObject Player;
    public float RotationSpeed = 100f; // Augmenté pour une meilleure réactivité

    private Vector3 offset; // Stocke la position relative de la caméra par rapport au centre de masse

    void Start()
    {
        if (Player == null) return;

        Rigidbody rb = Player.GetComponent<Rigidbody>();
        if (rb == null) return;

        // Calculer l'offset initial entre la caméra et le centre de masse
        offset = transform.position - rb.worldCenterOfMass;
    }

    void LateUpdate()
    {
        if (Player == null) return;

        Rigidbody rb = Player.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 centerOfMass = rb.worldCenterOfMass;

        float rotationInput = 0f;

        // Vérification des mouvements de la souris pour ajuster l'angle
        if (Input.mousePosition.x < Screen.width * ScreenInteractionFraction)
        {
            rotationInput = 1f;
        }
        else if (Input.mousePosition.x > Screen.width * (1 - ScreenInteractionFraction))
        {
            rotationInput = -1f;
        }

        // Rotation autour du centre de masse
        Quaternion rotation = Quaternion.AngleAxis(rotationInput * RotationSpeed * Time.deltaTime, Vector3.up);
        offset = rotation * offset;

        // Nouvelle position de la caméra
        transform.position = centerOfMass + offset;

        // Regarder le joueur
        transform.LookAt(centerOfMass);
    }
}
