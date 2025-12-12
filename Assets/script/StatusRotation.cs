using UnityEngine;
using UnityEngine.Audio;

public class StatusRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 40f; // vitesse de rotation
    [SerializeField] private float snapAngle = 30f; // angle pour l'accrochage
    [SerializeField] private GameObject rayIn; // rayon entrant
    [SerializeField] private GameObject rayOut; // rayon sortant
    [SerializeField] private int requiredStep; // étape requise pour activer le rayon
    [SerializeField] private AudioClip rotationSound; // son de rotation

    private AudioSource audioSource;
    private bool isDragging = false;
    private float lastMouseX;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        lastMouseX = Input.mousePosition.x;

        if (audioSource != null && rotationSound != null)
        {
            audioSource.clip = rotationSound;
            audioSource.Play();
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

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

    private void SnapToIncrement()
    {
        float currentY = transform.eulerAngles.y;
        float snappedY = Mathf.Round(currentY / snapAngle) * snapAngle;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, snappedY, transform.eulerAngles.z);
    }

    private void UpdateOutputRay()
    {
        if (rayIn == null || rayOut == null) return;

        if (!rayIn.activeSelf)
        {
            rayOut.SetActive(false);
            return;
        }

        float step = Mathf.Round(transform.eulerAngles.y / snapAngle);
        rayOut.SetActive(step == requiredStep);
    }
}
