using UnityEngine;

public class GearDrag : MonoBehaviour
{
    public int gearID = 0;

    private Camera cam;
    private Vector3 offset;
    private float dist;
    private Vector3 originalPos;
    private Vector3 originalScale;
    private Transform originalParent;

    private bool dragging = false;

    void Start()
    {
        cam = Camera.main;
        originalPos = transform.position;
        originalParent = transform.parent;
        originalScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        originalPos = transform.position;
        originalParent = transform.parent;

        dist = Vector3.Distance(cam.transform.position, transform.position);
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist));
        offset = transform.position - mouseWorld;

        dragging = true;
    }

    private void OnMouseDrag()
    {
        if (!dragging) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist));
        transform.position = mouseWorld + offset;
        
    }

    private void OnMouseUp()
    {
        dragging = false;

        // On demande le snap
        bool snapped = SnapManager.Instance.TrySnap(this);

        // Si snap raté → reset
        if (!snapped)
        {
            transform.position = originalPos;
            transform.SetParent(originalParent);
            // Remet la taille à l'original (1,1,1)
            transform.localScale = originalScale;
        }
    }
}
