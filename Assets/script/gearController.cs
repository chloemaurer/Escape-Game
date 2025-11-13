using System.Collections.Generic;
using UnityEngine;

public class SnapManager : MonoBehaviour
{
    public static SnapManager Instance;
    public float snapDistance = 1f; // Ajuste selon la taille du niveau
    private List<SnapPoint> points = new List<SnapPoint>();

    private void Awake()
    {
        Instance = this;
        points.AddRange(FindObjectsOfType<SnapPoint>());
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

                    gear.transform.localScale = Vector3.one * 2; // double la taille

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
}
