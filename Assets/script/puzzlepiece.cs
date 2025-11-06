using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector2 startPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private PuzzleManager puzzleManager;
    private RectTransform canvasObject;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasObject = GetComponentInParent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.8f; // Transparence pendant le drag
        canvasGroup.blocksRaycasts = false; // Permet de détecter les autres pièces
        transform.SetAsLastSibling(); // Passe au-dessus des autres
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            // ➤ déplacement uniquement sur l’axe X
            //rectTransform.anchoredPosition = new Vector2(pos.x, startPosition.y);
            rectTransform.anchoredPosition = new Vector2(Mathf.Clamp(pos.x, canvasObject.rect.position.x*3.5f, 3.5f*(canvasObject.rect.position.x + canvasObject.rect.size.x)), startPosition.y);
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        PuzzlePiece otherPiece = eventData.pointerDrag.GetComponent<PuzzlePiece>();
        if (otherPiece != null && otherPiece != this)
        {
            // Sauvegarde les positions
            Vector2 tempPos = rectTransform.anchoredPosition;

            // Échange les positions
            //rectTransform.anchoredPosition = otherPiece.startPosition;
            rectTransform.anchoredPosition = otherPiece.startPosition;
            otherPiece.rectTransform.anchoredPosition = tempPos;

            // Met à jour leurs positions de départ
            Vector2 tempStart = startPosition;
            startPosition = otherPiece.startPosition;
            otherPiece.startPosition = tempStart;

            // Vérifie le puzzle après l’échange
            if (puzzleManager != null)
                puzzleManager.CheckPuzzle();
        }


    }
}
