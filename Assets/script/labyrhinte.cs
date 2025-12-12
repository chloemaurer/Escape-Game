using UnityEngine;

public class labyrhinte : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // vitesse de rotation en degrés par seconde
    [SerializeField] private float lerpSmooth = 5f;     // vitesse d'interpolation pour le lissage

    private float targetAngleZ = 0f;  // angle cible
    private float currentAngleZ = 0f; // angle actuel

    void Update()
    {
        // ajuste l'angle cible selon les flèches gauche/droite
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetAngleZ += rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            targetAngleZ -= rotationSpeed * Time.deltaTime;
        }

        // interpolation pour un mouvement fluide
        currentAngleZ = Mathf.LerpAngle(currentAngleZ, targetAngleZ, Time.deltaTime * lerpSmooth);

        // applique la rotation sur l'axe Z
        transform.localEulerAngles = new Vector3(0f, 0f, currentAngleZ);
    }
}
