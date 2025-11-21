using UnityEngine;

public class labyrhinthe : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // degré par seconde
    [SerializeField] private float lerpSmooth = 5f; // vitesse d'interpolation

    private float targetAngleZ = 0f; // angle souhaité
    private float currentAngleZ = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetAngleZ += rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            targetAngleZ -= rotationSpeed * Time.deltaTime;
        }

        currentAngleZ = Mathf.LerpAngle(currentAngleZ, targetAngleZ, Time.deltaTime * lerpSmooth);
        transform.localEulerAngles = new Vector3(0f, 0f, currentAngleZ);
    }
}
