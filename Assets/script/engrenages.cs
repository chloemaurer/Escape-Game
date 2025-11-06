using UnityEngine;

public class GearRotation : MonoBehaviour
{
    [Header("Paramètres de rotation")]
    public float rotationSpeed = 50f;  // Vitesse de rotation en degrés/seconde
    public bool clockwise = true;      // Sens de rotation

    void Update()
    {
        float direction = clockwise ? -1f : 1f;
        transform.Rotate(0, 0, rotationSpeed * direction * Time.deltaTime);
    }
}
