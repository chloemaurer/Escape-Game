using System.Collections.Generic;
using UnityEngine;

public class ClocheController : MonoBehaviour
{
    public static ClocheController Instance;

    [SerializeField] private AudioClip fireSound;    // son joué quand un feu s'allume
    [SerializeField] private AudioClip tunnelSound;  // son joué lors de l'ouverture du tunnel
    [SerializeField] private Transform fireSetsParent; // parent contenant tous les sets de feux
    [SerializeField] private Animator porteAnimation;  // animation de la porte du tunnel

    private AudioSource audioSource;

    // liste des séquences de cloches à valider
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
        // désactive tous les sets de feux au départ
        for (int i = 0; i < fireSetsParent.childCount; i++)
            fireSetsParent.GetChild(i).gameObject.SetActive(false);

        // si la séquence de feu 1 est déjà résolue
        if (PlayerPrefs.GetInt("fireset1", 0) == 1)
        {
            currentSequence = 0;
            ShowFireSet(currentSequence); // active le premier set
        }
        else
        {
            currentSequence = 0; // sinon on ne montre rien
        }
    }

    // affiche le set de feux correspondant à la séquence actuelle
    private void ShowFireSet(int index)
    {
        for (int i = 0; i < fireSetsParent.childCount; i++)
            fireSetsParent.GetChild(i).gameObject.SetActive(false);

        currentFireSet = fireSetsParent.GetChild(index);
        currentFireSet.gameObject.SetActive(true);

        for (int i = 0; i < currentFireSet.childCount; i++)
            currentFireSet.GetChild(i).gameObject.SetActive(true);
    }

    // joue un son en 2D
    private void PlaySound2D(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        GameObject temp = new GameObject("TempAudio");
        AudioSource a = temp.AddComponent<AudioSource>();
        a.clip = clip;
        a.volume = volume;
        a.spatialBlend = 0f; // 2D : pas affecté par la distance
        a.Play();
        Destroy(temp, clip.length);
    }

    // méthode appelée quand le joueur tape une cloche
    public void RegisterNote(int bellID)
    {
        if (currentSequence >= sequences.Count)
        {
            Debug.Log("Toutes les séquences ont déjà été terminées.");
            return;
        }

        List<int> seq = sequences[currentSequence];

        if (bellID == seq[currentIndex])
        {
            // note correcte
            currentIndex++;
            Debug.Log("Note correcte : " + bellID);

            if (currentIndex >= seq.Count)
            {
                Debug.Log("Séquence " + (currentSequence + 1) + " réussie.");

                audioSource.clip = fireSound;
                audioSource.Play();

                currentSequence++;
                currentIndex = 0;

                if (currentSequence < sequences.Count)
                {
                    ShowFireSet(currentSequence);
                }
                else
                {
                    // toutes les séquences terminées
                    porteAnimation.SetTrigger("ouvertureTunnel");
                    PlaySound2D(tunnelSound, 1f);
                    Debug.Log("Toutes les séquences sont complétées !");
                }
            }
        }
        else
        {
            // note incorrecte
            Debug.Log("Mauvaise note : " + bellID + " | Attendu = " + seq[currentIndex]);

            // reset de la séquence
            currentIndex = 0;
            ShowFireSet(currentSequence);

            // si la note tapée correspond au début de la séquence, on reprend
            if (bellID == seq[0])
            {
                currentIndex = 1;
                Debug.Log("La note correspond au début de la séquence, on reprend ici.");
            }
        }
    }
}
