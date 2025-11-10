using System.Collections.Generic;
using UnityEngine;

public class SnapManager : MonoBehaviour
{
    public static SnapManager Instance;
    private List<SnapPoint> snapPoints = new List<SnapPoint>();

    private void Awake()
    {
        Instance = this;
        snapPoints.AddRange(FindObjectsOfType<SnapPoint>());
    }

    public bool TrySnapGear(DraggableGear gear)
    {
        foreach (var snap in snapPoints)
        {
            if (snap.isOccupied) continue;

            float distance = Vector3.Distance(gear.transform.position, snap.transform.position);

            // Si l'engrenage est proche du point de snap
            if (distance < 80f) // Ajuste selon la taille de ton canvas
            {
                if (gear.GetGearID() == snap.requiredGearID)
                {
                    gear.transform.position = snap.transform.position;
                    gear.transform.SetParent(snap.transform);
                    snap.isOccupied = true;
                    CheckAllPlaced();
                    return true;
                }
                else
                {
                    // Mauvais engrenage
                    Debug.Log("Mauvais engrenage !");
                    return false;
                }
            }
        }
        return false;
    }

    private void CheckAllPlaced()
    {
        foreach (var snap in snapPoints)
        {
            if (!snap.isOccupied) return;
        }

        // Tous les points sont remplis
        Debug.Log("✅ Tous les engrenages sont bien placés ! Niveau réussi !");
    }
}
