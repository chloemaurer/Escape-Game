using UnityEngine;

public class StatusRotationManage : MonoBehaviour
{
    public GameObject[] statusPrefabs; // Assignés dans l'inspector
    private GameObject currentStatue;

    void Start()
    {
        string id = PlayerPrefs.GetString("SelectedStatue");

        // Trouver le prefab correspondant
        foreach (GameObject prefab in statusPrefabs)
        {
            if (prefab.name == id) // nom = identique à l'ID
            {
                currentStatue = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                break;
            }
        }

        if (currentStatue != null)
        {
            // Ajoute automatiquement le script de rotation
            currentStatue.AddComponent<StatusRotation>();
        }
        else
        {
            Debug.LogError("Aucun prefab trouvé pour l'ID : " + id);
        }
    }
}
