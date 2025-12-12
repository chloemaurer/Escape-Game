using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField, Range(0, 1)]
    private float screenInteractionFraction = 0.1f; // portion de l'écran pour déclencher la rotation

    [SerializeField] private GameObject player; // le joueur à suivre
    [SerializeField] private float rotationSpeed = 100f; // vitesse de rotation de la caméra

    private Vector3 offset; // position relative de la caméra par rapport au centre de masse du joueur

    void Start()
    {
        if (player == null) return;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb == null) return;

        // calcul initial de la distance entre la caméra et le centre de masse du joueur
        offset = transform.position - rb.worldCenterOfMass;
    }

    void LateUpdate()
    {
        if (player == null) return;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3 centerOfMass = rb.worldCenterOfMass;
        float rotationInput = 0f;

        // regarde la position de la souris pour déterminer si on doit tourner la caméra
        if (Input.mousePosition.x < Screen.width * screenInteractionFraction)
        {
            rotationInput = 1f;
        }
        else if (Input.mousePosition.x > Screen.width * (1 - screenInteractionFraction))
        {
            rotationInput = -1f;
        }

        // applique la rotation autour du centre de masse
        Quaternion rotation = Quaternion.AngleAxis(rotationInput * rotationSpeed * Time.deltaTime, Vector3.up);
        offset = rotation * offset;

        // nouvelle position de la caméra
        transform.position = centerOfMass + offset;

        // orienter la caméra vers le joueur
        transform.LookAt(centerOfMass);
    }
}
