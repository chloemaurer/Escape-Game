using UnityEngine;

public class StatusRotationManage : MonoBehaviour
{
    public Transform StatusParent;  // parent contenant toutes les statues

    private GameObject currentStatue;

    void Start()
    {
        string id = PlayerPrefs.GetString("SelectedStatue", "null");
        Debug.Log("ID reçu dans la scene rotation : " + id);

        currentStatue = null;

        // Désactive toutes les statues et cherche celle à activer
        for (int i = 0; i < StatusParent.childCount; i++)
        {
            GameObject child = StatusParent.GetChild(i).gameObject;
            if (child.name == id)
            {
                currentStatue = child;
                child.SetActive(true);
            }
            else
            {
                child.SetActive(false);
            }
        }

        if (currentStatue == null)
        {
            Debug.LogError("Aucune statue trouvée pour l'ID : " + id);
        }
        else
        {
            // Ajoute le script de rotation si nécessaire
            if (currentStatue.GetComponent<StatusRotate>() == null)
                currentStatue.AddComponent<StatusRotate>();
        }
    }
}
