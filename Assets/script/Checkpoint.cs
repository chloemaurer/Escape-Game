using UnityEngine;
using System.Collections.Generic; // 🔑 N'oubliez pas ceci !

public class Checkpoint : MonoBehaviour
{
    [Header("Nom unique du checkpoint")]
    public string checkpointName;

    // 🔑 MAPPING STATIQUE : Stocke la référence Transform de chaque checkpoint.
    // Cette variable statique persiste même après les changements de scène.
    private static Dictionary<string, Transform> AllCheckpoints = new Dictionary<string, Transform>();

    void OnEnable()
    {
        // Enregistrer ou mettre à jour la référence dès l'activation
        if (AllCheckpoints.ContainsKey(checkpointName))
        {
            AllCheckpoints[checkpointName] = transform;
        }
        else
        {
            AllCheckpoints.Add(checkpointName, transform);
        }
        // Pour debuguer :
        // Debug.Log($"[CHECKPOINT MANAGER] Enregistré/Mis à jour : {checkpointName}");
    }

    // Assure que l'objet est bien retiré de la mémoire si la scène se décharge complètement.
    void OnDisable()
    {
        if (AllCheckpoints.ContainsKey(checkpointName) && AllCheckpoints[checkpointName] == transform)
        {
            AllCheckpoints.Remove(checkpointName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Sauvegarde de la clé (le nom)
            PlayerPrefs.SetString("LastCheckpoint", checkpointName);
            PlayerPrefs.Save();
            Debug.Log($"Checkpoint activé (Sauvegardé) : {checkpointName}");
        }
    }

    public static Transform GetSavedCheckpoint()
    {
        string lastCheckpoint = PlayerPrefs.GetString("LastCheckpoint", "");

        if (lastCheckpoint == "")
            return null; // Pas de checkpoint sauvegardé

        // 1. 🔑 Tenter la récupération rapide et fiable depuis la liste statique (en mémoire)
        if (AllCheckpoints.ContainsKey(lastCheckpoint))
        {
            Debug.Log($"[SPAWN MANAGER] Respawn trouvé (mémoire statique) : {lastCheckpoint}");
            return AllCheckpoints[lastCheckpoint];
        }

        // 2. Tenter la recherche par FindObjectsOfType (méthode de secours)
        Checkpoint[] all = FindObjectsOfType<Checkpoint>();

        foreach (Checkpoint cp in all)
        {
            if (cp.checkpointName == lastCheckpoint)
            {
                // Si trouvé tardivement, l'ajouter à la liste statique pour les prochaines fois
                if (!AllCheckpoints.ContainsKey(cp.checkpointName))
                {
                    AllCheckpoints.Add(cp.checkpointName, cp.transform);
                }
                Debug.Log($"[SPAWN MANAGER] Respawn trouvé (recherche tardive) : {lastCheckpoint}");
                return cp.transform;
            }
        }

        Debug.LogError($"Checkpoint non trouvé dans la scène : {lastCheckpoint}. Le joueur spawnra à l'origine de la scène.");
        return null;
    }
}