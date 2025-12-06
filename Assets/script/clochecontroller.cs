using System.Collections.Generic;
using UnityEngine;

public class ClocheController : MonoBehaviour
{
    public static ClocheController Instance;
    public AudioClip fireSound;
    private AudioSource audioSource;
    [Header("Parent contenant les 5 sets de feux")]
    public Transform fireSetsParent;

    // Séquences des cloches
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
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        // Désactiver tous les feux au début
        for (int i = 0; i < fireSetsParent.childCount; i++)
            fireSetsParent.GetChild(i).gameObject.SetActive(false);

        // Si l'énigme des engrenages est résolue
        if (PlayerPrefs.GetInt("fireset1", 0) == 1)
        {
            // On active le premier FireSet (index 0)
            currentSequence = 0;
            ShowFireSet(currentSequence);
        }
        else
        {
            // Sinon : NE RIEN AFFICHER (aucun feu)
            currentSequence = 0;
        }
    }


    // Affiche le set de feux correspondant à la séquence actuelle
    private void ShowFireSet(int index)
    {
        for (int i = 0; i < fireSetsParent.childCount; i++)
            fireSetsParent.GetChild(i).gameObject.SetActive(false);

        currentFireSet = fireSetsParent.GetChild(index);
        currentFireSet.gameObject.SetActive(true);

        for (int i = 0; i < currentFireSet.childCount; i++)
            currentFireSet.GetChild(i).gameObject.SetActive(true);
    }

    // Méthode pour enregistrer la note tapée par le joueur
    public void RegisterNote(int bellID)
    {
        if (currentSequence >= sequences.Count)
        {
            Debug.Log("Toutes les séquences ont déjà été réussies !");
            return;
        }

        List<int> seq = sequences[currentSequence];

        if (bellID == seq[currentIndex])
        {
            // Bonne note
            currentIndex++;
            Debug.Log("✅ Note correcte ! Bell = " + bellID);

            if (currentIndex >= seq.Count)
            {
                Debug.Log("🎉 Séquence " + (currentSequence + 1) + " réussie !");

                // Jouer le son de feu
                audioSource.clip = fireSound;
                audioSource.Play();

                currentSequence++;
                currentIndex = 0;

                if (currentSequence < sequences.Count)
                    ShowFireSet(currentSequence);
                else
                    Debug.Log("🎊 Toutes les séquences terminées !");
                    PlayerPrefs.SetInt("tunnelopen", 1);
            }
        }
        else
        {
            // Mauvaise note
            Debug.Log("❌ Mauvaise note ! Bell = " + bellID +
                      " | Attendu = " + seq[currentIndex]);

            // Reset de la séquence
            currentIndex = 0;
            ShowFireSet(currentSequence);

            // Vérifie si la note tapée correspond au début de la séquence
            if (bellID == seq[0])
            {
                currentIndex = 1;
                Debug.Log("➡ La note tapée correspond au début de la séquence, reprise !");
            }
        }
    }
}
