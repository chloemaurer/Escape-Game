using UnityEngine;

public class GearRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f; // vitesse de rotation en degrés par seconde
    [SerializeField] private bool clockwise = true;     // indique le sens de rotation

    void Update()
    {
        // calcule la direction en fonction du sens choisi
        float direction = clockwise ? -1f : 1f;

        // applique la rotation sur l'axe Z
        transform.Rotate(0, 0, rotationSpeed * direction * Time.deltaTime);
    }
}
