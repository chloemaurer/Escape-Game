using UnityEngine;

public class StatusRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private bool isDragging = false;
    private float lastMouseX;

    void Update()
    {
        // Début du clic
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMouseX = Input.mousePosition.x;
        }

        // Fin du clic
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Rotation pendant le drag
        if (isDragging)
        {
            float deltaX = Input.mousePosition.x - lastMouseX;

            // Rotation autour de Y
            transform.Rotate(0f, deltaX * rotationSpeed * Time.deltaTime, 0f);

            lastMouseX = Input.mousePosition.x;
        }
    }
}
