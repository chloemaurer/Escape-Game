using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableGear : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;
    private Canvas canvas;

    [SerializeField] private int gearID; // Identifiant de l'engrenage

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(canvas.transform); // Pour que ça reste visible
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Si aucun SnapPoint détecté, on revient à la position de départ
        if (!SnapManager.Instance.TrySnapGear(this))
        {
            transform.position = startPosition;
            transform.SetParent(originalParent);
        }
    }

    public int GetGearID()
    {
        return gearID;
    }
}
