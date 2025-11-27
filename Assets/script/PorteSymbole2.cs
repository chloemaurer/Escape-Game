using UnityEngine;
using UnityEngine.EventSystems;

public class PorteSymbole2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] private float rotationSpeed = 25f; // ajustable dans l'inspecteur
    private Vector2 startPos;
    private bool isDragging = false;
    [SerializeField] private PorteSymboleManager manager;
    public AudioClip moveRockSound;
    private AudioSource audioSource;

    private void Awake()
    {
        Debug.Log("🚪 PorteInca3D initialisée.");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        startPos = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (!isDragging) return;

        audioSource.clip = moveRockSound;
        audioSource.Play();
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
