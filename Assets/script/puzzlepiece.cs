using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector2 startPosition;         // position initiale avant le drag
    private RectTransform rectTransform;   // rectTransform de la pièce
    private Canvas canvas;                 // canvas parent
    private CanvasGroup canvasGroup;       // permet la transparence et blocage des raycasts
    private PuzzleManager puzzleManager;   // référence au PuzzleManager
    private RectTransform canvasObject;    // rectTransform du canvas parent

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasObject = GetComponentInParent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;

        // rendre la pièce semi-transparente pendant le drag
        canvasGroup.alpha = 0.8f;

        // permettre aux autres pièces de recevoir le raycast
        canvasGroup.blocksRaycasts = false;

        // mettre la pièce au-dessus des autres
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            // déplacement limité à l’axe X
            rectTransform.anchoredPosition = new Vector2(
                Mathf.Clamp(pos.x, canvasObject.rect.position.x * 3.5f,
                3.5f * (canvasObject.rect.position.x + canvasObject.rect.size.x)),
                startPosition.y);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // rétablir l’opacité et les raycasts
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        PuzzlePiece otherPiece = eventData.pointerDrag.GetComponent<PuzzlePiece>();
        if (otherPiece != null && otherPiece != this)
        {
            // sauvegarde la position actuelle
            Vector2 tempPos = rectTransform.anchoredPosition;

            // échange des positions
            rectTransform.anchoredPosition = otherPiece.startPosition;
            otherPiece.rectTransform.anchoredPosition = tempPos;

            // échange des positions de départ
            Vector2 tempStart = startPosition;
            startPosition = otherPiece.startPosition;
            otherPiece.startPosition = tempStart;

            // vérifie si le puzzle est complet après l’échange
            if (puzzleManager != null)
                puzzleManager.CheckPuzzle();
        }
    }
}
