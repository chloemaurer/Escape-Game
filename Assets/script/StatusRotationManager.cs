using UnityEngine;

public class StatusRotationManage : MonoBehaviour
{
    [SerializeField] private Transform statusParent; // parent contenant toutes les statues

    private GameObject currentStatue;

    private void Start()
    {
        string id = PlayerPrefs.GetString("SelectedStatue", "null");
        Debug.Log("ID reçu dans la scène rotation : " + id);

        currentStatue = null;

        // Parcourt toutes les statues et active uniquement celle sélectionnée
        for (int i = 0; i < statusParent.childCount; i++)
        {
            GameObject child = statusParent.GetChild(i).gameObject;

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
            // Si le script de rotation n'est pas déjà présent, l'ajoute
            if (currentStatue.GetComponent<StatusRotate>() == null)
                currentStatue.AddComponent<StatusRotate>();
        }
    }
}
