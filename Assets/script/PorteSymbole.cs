using UnityEngine;
using UnityEngine.EventSystems;

public class PorteInca3D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float rotationSpeed = 25f;     // vitesse de rotation lors du drag
    [SerializeField] private PorteSymboleManager manager;   // manager pour vérifier l'alignement
    [SerializeField] private AudioClip moveRockSound;       // son joué pendant le drag

    private AudioSource audioSource;  // source audio pour jouer le son
    private Vector2 startPos;         // position initiale du clic
    private bool isDragging = false;  // indique si le joueur est en train de draguer

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // début du drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (moveRockSound != null)
        {
            audioSource.clip = moveRockSound;
            audioSource.Play();
        }

        startPos = eventData.position;
        isDragging = true;
    }

    // drag en cours
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        float deltaX = eventData.position.x - startPos.x;

        // rotation autour de l'axe Z
        transform.Rotate(0f, 0f, -deltaX * rotationSpeed * Time.deltaTime, Space.Self);

        startPos = eventData.position;
    }

    // fin du drag
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        if (manager != null)
            manager.CheckAlignment();
        else
            Debug.LogWarning("PorteSymboleManager non assigné dans PorteInca3D.");
    }
}
