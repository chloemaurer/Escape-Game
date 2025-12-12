using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnapManager : MonoBehaviour
{
    public static SnapManager Instance;

    [Header("Distance max pour que le snap fonctionne")]
    public float snapDistance = 1f; // Ajuste selon la taille du niveau

    private List<SnapPoint> points = new List<SnapPoint>();

    private void Awake()
    {
        Instance = this;
        points.AddRange(FindObjectsOfType<SnapPoint>());

        // Réinitialisation des prefs
        PlayerPrefs.SetInt("labyrintheDoorOpened", 0);
        PlayerPrefs.SetInt("fireset1", 0);
    }

    // Tente de snapper un engrenage sur un point
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

    // Vérifie si tous les engrenages sont bien placés
    public void Checkfin()
    {
        foreach (var p in points)
        {
            GearDrag gear = p.GetComponentInChildren<GearDrag>();
            if (gear == null)
            {
                Debug.Log("❌ Aucun engrenage sur le point " + p.name);
                return; // pas fini
            }

            if (gear.gearID != p.requiredGearID)
            {
                Debug.Log("❌ Mauvais engrenage sur le point " + p.name);
                return;
            }
        }

        // Tous les points sont corrects
        Debug.Log("🎉 Tous les engrenages sont en place !");
        PlayerPrefs.SetInt("labyrintheDoorOpened", 1);

        if (Application.CanStreamedLevelBeLoaded("Escape Game"))
        {
            FirePlay();
            SceneManager.LoadScene("Escape Game");
        }
        else
        {
            Debug.LogError("❌ La scène 'Escape Game' n'est pas dans les Build Settings !");
        }
    }

    // Active le feu du puzzle
    private static void FirePlay()
    {
        PlayerPrefs.SetInt("fireset1", 1);
    }
}
