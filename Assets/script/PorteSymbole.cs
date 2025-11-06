using UnityEngine;
using UnityEngine.EventSystems;

public class PorteInca3D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float rotationSpeed = 25f; // ajustable dans l'inspecteur
    private Vector2 startPos;
    private bool isDragging = false;
    [SerializeField] private PorteSymboleManager manager;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        float deltaX = eventData.position.x - startPos.x;

        // rotation autour de l'axe Z
        transform.Rotate(0f, 0f, -deltaX * rotationSpeed * Time.deltaTime, Space.Self);

        startPos = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        if (manager != null)
        {
            manager.CheckAlignment();
        }
        else
        {
            Debug.LogWarning("⚠️ PorteIncaManager non assigné dans PorteInca3D.");
        }
    }
}
