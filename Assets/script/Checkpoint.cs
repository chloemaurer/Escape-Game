using UnityEngine;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private string checkpointName; // nom unique de ce checkpoint

    // dictionnaire statique qui garde la référence de tous les checkpoints
    // persiste entre les scènes
    private static Dictionary<string, Transform> allCheckpoints = new Dictionary<string, Transform>();

    void OnEnable()
    {
        // ajoute ou met à jour ce checkpoint dans le dictionnaire
        if (allCheckpoints.ContainsKey(checkpointName))
        {
            allCheckpoints[checkpointName] = transform;
        }
        else
        {
            allCheckpoints.Add(checkpointName, transform);
        }

        // debug : vérifie l'enregistrement du checkpoint
        // Debug.Log("Checkpoint enregistré ou mis à jour : " + checkpointName);
    }

    void OnDisable()
    {
        // supprime ce checkpoint si la scène se décharge
        if (allCheckpoints.ContainsKey(checkpointName) && allCheckpoints[checkpointName] == transform)
        {
            allCheckpoints.Remove(checkpointName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // sauvegarde ce checkpoint comme dernier atteint
            PlayerPrefs.SetString("LastCheckpoint", checkpointName);
            PlayerPrefs.Save();

            Debug.Log("Checkpoint activé et sauvegardé : " + checkpointName);
        }
    }

    public static Transform GetSavedCheckpoint()
    {
        string lastCheckpoint = PlayerPrefs.GetString("LastCheckpoint", "");

        if (lastCheckpoint == "")
            return null; // aucun checkpoint sauvegardé

        // tentative de récupération rapide depuis le dictionnaire statique
        if (allCheckpoints.ContainsKey(lastCheckpoint))
        {
            Debug.Log("Respawn trouvé dans la mémoire : " + lastCheckpoint);
            return allCheckpoints[lastCheckpoint];
        }

        // méthode de secours : recherche dans tous les objets Checkpoint de la scène
        Checkpoint[] all = FindObjectsOfType<Checkpoint>();

        foreach (Checkpoint cp in all)
        {
            if (cp.checkpointName == lastCheckpoint)
            {
                // ajoute au dictionnaire pour les prochaines fois
                if (!allCheckpoints.ContainsKey(cp.checkpointName))
                {
                    allCheckpoints.Add(cp.checkpointName, cp.transform);
                }

                Debug.Log("Respawn trouvé après recherche dans la scène : " + lastCheckpoint);
                return cp.transform;
            }
        }

        Debug.LogError("Checkpoint non trouvé dans la scène : " + lastCheckpoint + ". Le joueur spawnera à l'origine.");
        return null;
    }
}
