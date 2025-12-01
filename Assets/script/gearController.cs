using System.Collections.Generic;
using Unity.VisualScripting;
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
            // Vérifie si un engrenage est enfant du SnapPoint
            if (p.transform.childCount == 0)
            {
                Debug.Log("❌ Un point n'a pas d'engrenage.");
                return; // pas fini
            }

            // Récupère l'engrenage snapé
            GearDrag gear = p.GetComponentInChildren<GearDrag>();

            if (gear == null)
            {
                Debug.Log("❌ Un enfant n'est pas un engrenage.");
                return;
            }

            // Vérifie l'ID
            if (gear.gearID != p.requiredGearID)
            {
                Debug.Log("❌ Mauvais engrenage sur un point.");
                return;
            }
        }

        // Si on arrive ici → TOUT EST OK
        Debug.Log("🎉 FIN DU JEU !");
    }

}
