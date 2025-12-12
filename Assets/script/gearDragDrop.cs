using UnityEngine;

public class GearDrag : MonoBehaviour
{
    [SerializeField] public int gearID = 0; // identifiant de l'engrenage

    private Camera cam;
    private Vector3 offset;
    private float dist;
    private Vector3 originalPos;
    private Vector3 originalScale;
    private Transform originalParent;

    private bool dragging = false;
    public bool canDrag = true; // indique si l'engrenage peut être déplacé

    void Start()
    {
        cam = Camera.main;
        originalPos = transform.position;
        originalParent = transform.parent;
        originalScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (!canDrag) return;

        // sauvegarde de la position et du parent d'origine
        originalPos = transform.position;
        originalParent = transform.parent;

        dist = Vector3.Distance(cam.transform.position, transform.position);
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist));
        offset = transform.position - mouseWorld;

        dragging = true; // début du drag
    }

    private void OnMouseDrag()
    {
        if (!dragging) return;

        // suit la souris avec l'offset initial
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist));
        transform.position = mouseWorld + offset;
    }

    private void OnMouseUp()
    {
        if (!dragging) return;
        dragging = false;

        // tente de snapper l'engrenage sur un point
        bool snapped = SnapManager.Instance.TrySnap(this);

        if (snapped)
        {
            // si tous les engrenages sont placés, vérifie la fin du puzzle
            SnapManager.Instance.Checkfin();
        }
        else
        {
            // snap raté : retour à la position d'origine
            transform.position = originalPos;
            transform.SetParent(originalParent);
            transform.localScale = originalScale;
        }
    }
}
