using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{

    public RawImage[] pieces;

    public float tolerance = 10f;

    public void CheckPuzzle()
    {
        // Vérifie si chaque pièce est bien à gauche de la suivante
        for (int i = 0; i < pieces.Length - 1; i++)
        {
            float x1 = pieces[i].rectTransform.anchoredPosition.x;
            float x2 = pieces[i + 1].rectTransform.anchoredPosition.x;

            if (x1 > x2 + tolerance)
            {
                Debug.Log("Puzzle pas terminé");
                return;
            }
        }

        Debug.Log("Puzzle finit");
    }
}
