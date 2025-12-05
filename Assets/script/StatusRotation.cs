using UnityEngine;

public class StatusRotate : MonoBehaviour
{
    public float rotationSpeed = 40f;
    private bool isDragging = false;
    private float lastMouseX;

    public float snapAngle = 30f;

    public GameObject rayIn;
    public GameObject rayOut;

    public int requiredStep;

    // --------------------------------------------------
    // UNIQUEMENT quand la souris clique sur CETTE statue
    // --------------------------------------------------
    private void OnMouseDown()
    {
        isDragging = true;
        lastMouseX = Input.mousePosition.x;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    // --------------------------------------------------

    private void Update()
    {
        if (isDragging)
        {
            float deltaX = Input.mousePosition.x - lastMouseX;
            transform.Rotate(0f, -deltaX * rotationSpeed * Time.deltaTime, 0f);
            lastMouseX = Input.mousePosition.x;
        }
        else
        {
            SnapToIncrement();
        }

        UpdateOutputRay();
    }

    // --------------------------------------------------

    private void SnapToIncrement()
    {
        float currentY = transform.eulerAngles.y;
        float snappedY = Mathf.Round(currentY / snapAngle) * snapAngle;

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            snappedY,
            transform.eulerAngles.z
        );
    }

    // --------------------------------------------------

    private void UpdateOutputRay()
    {
        if (!rayIn.activeSelf)
        {
            rayOut.SetActive(false);
            return;
        }

        float step = Mathf.Round(transform.eulerAngles.y / snapAngle);

        rayOut.SetActive(step == requiredStep);
    }
}
