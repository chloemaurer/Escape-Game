using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnapManager : MonoBehaviour 
{
    public static SnapManager Instance;
    public float snapDistance = 1f; // Ajuste selon la taille du niveau
    private List<SnapPoint> points = new List<SnapPoint>();

    private void Awake()
    {
        Instance = this;
        points.AddRange(FindObjectsOfType<SnapPoint>());
        PlayerPrefs.SetInt("GearDoorOpened", 0);

    }

    public bool TrySnap(GearDrag gear)
    {
        foreach (var p in points)
        {
            float dist = Vector3.Distance(gear.transform.position, p.transform.position);

            if (dist <= snapDistance)
            {
                if (gear.gearID == p.requiredGearID)
                {
                    // Snap réussi
                    gear.transform.position = p.transform.position;
                    gear.transform.rotation = p.transform.rotation;
                    gear.transform.SetParent(p.transform);
                    gear.canDrag = false; // Désactive le drag après le snap
                    gear.transform.localScale *= 2; // double la taille
                   
                    
                    // ⚡ On ne touche pas le scale ici
                    return true;
                }
                else
                {
                    // Mauvais ID → retour false
                    return false;
                }
            }
        }

        // Trop loin de tous les points
        return false;
    }

    public void Checkfin()
    {
        foreach (var p in points)
        {
            // Vérifie qu'un GearDrag est présent en enfant
            GearDrag gear = p.GetComponentInChildren<GearDrag>();
            if (gear == null)
            {
                Debug.Log("❌ Aucun engrenage sur le point " + p.name);
                return; // pas fini
            }

            // Vérifie que l'ID correspond
            if (gear.gearID != p.requiredGearID)
            {
                Debug.Log("❌ Mauvais engrenage sur le point " + p.name);
                return;
            }
        }

        // Tous les points ont le bon engrenage
        Debug.Log("🎉 Tous les engrenages sont en place !");
        PlayerPrefs.SetInt("GearDoorOpened", 1);

        // Vérifie que la scène existe dans Build Settings
        if (Application.CanStreamedLevelBeLoaded("Escape Game"))
        {
            fireplay();
            SceneManager.LoadScene("Escape Game");
        }
        else
        {
            Debug.LogError("❌ La scène 'Escape Game' n'est pas dans les Build Settings !");
        }
    }

    private static void fireplay()
    {
        PlayerPrefs.SetInt("fireset1", 1);
    }
}
