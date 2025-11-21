using System.Collections.Generic;
using UnityEngine;

public class ClocheController : MonoBehaviour
{
    public static ClocheController Instance;

    [Header("Parent contenant les 5 sets de feux")]
    public Transform fireSetsParent;

    private List<List<int>> sequences = new List<List<int>>()
    {
        new List<int>{1,3,5,1},
        new List<int>{2,6,5,2},
        new List<int>{3,5,4,3},
        new List<int>{6,1,2,5},
        new List<int>{6,4,2,3}
    };

    private int currentSequence = 0;
    private int currentIndex = 0;

    private Transform currentFireSet;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ActivateCurrentFireSet();
    }

    private void ActivateCurrentFireSet()
    {
        // Désactiver tous les sets
        for (int i = 0; i < fireSetsParent.childCount; i++)
            fireSetsParent.GetChild(i).gameObject.SetActive(false);

        // Activer seulement le set correspondant
        currentFireSet = fireSetsParent.GetChild(currentSequence);
        currentFireSet.gameObject.SetActive(true);

        // Éteindre ses 4 feux
        for (int i = 0; i < currentFireSet.childCount; i++)
            currentFireSet.GetChild(i).gameObject.SetActive(false);
    }

    public void RegisterNote(int bellID)
    {
        List<int> seq = sequences[currentSequence];

        // ✔ bonne note
        if (bellID == seq[currentIndex])
        {
            currentFireSet.GetChild(currentIndex).gameObject.SetActive(true);
            currentIndex++;

            // ✔ Séquence terminée
            if (currentIndex >= seq.Count)
            {
                Debug.Log("Séquence " + (currentSequence + 1) + " réussie !");
                currentSequence++;
                currentIndex = 0;

                // Terminé ?
                if (currentSequence >= sequences.Count)
                {
                    Debug.Log("🎉 Toutes les séquences réussies !");
                    return;
                }

                ActivateCurrentFireSet();
            }
        }
        else
        {
            Debug.Log("❌ Mauvaise note !");
            ResetSequence();
        }
    }

    private void ResetSequence()
    {
        currentIndex = 0;

        // Éteindre les feux du set actuel
        for (int i = 0; i < currentFireSet.childCount; i++)
            currentFireSet.GetChild(i).gameObject.SetActive(false);
    }
}
