using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private RawImage[] pieces;  // pièces du puzzle à vérifier
    [SerializeField] private float tolerance = 10f; // tolérance de placement en pixels

    private void Awake()
    {
        // réinitialisation de l'état du puzzle
        PlayerPrefs.SetInt("PuzzleCompleted", 0);
    }

    // vérifie si le puzzle est correctement assemblé
    public void CheckPuzzle()
    {
        for (int i = 0; i < pieces.Length - 1; i++)
        {
            float x1 = pieces[i].rectTransform.anchoredPosition.x;
            float x2 = pieces[i + 1].rectTransform.anchoredPosition.x;

            // si une pièce est mal placée → puzzle incomplet
            if (x1 > x2 + tolerance)
            {
                Debug.Log("Puzzle pas terminé");
                return;
            }
        }

        // toutes les pièces sont correctes → puzzle complet
        PuzzleComplet();
    }

    // actions à effectuer lorsque le puzzle est complété
    private void PuzzleComplet()
    {
        PlayerPrefs.SetInt("PuzzleCompleted", 1);

        // charger la scène suivante
        SceneManager.LoadScene("Escape Game");
    }
}
