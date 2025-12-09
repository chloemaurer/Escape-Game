using UnityEngine;

public class StatuesFin : MonoBehaviour
{
    public GameObject DernierRayon;
    public Animator GardenArch;
    private bool triggered = false;

    void Update()
    {
        if (!triggered && DernierRayon.activeInHierarchy)
        {
            triggered = true;
            Debug.Log("Dernier rayon actif, ouverture de l'arche du jardin.");
            GardenArch.SetTrigger("openGarden");
        }
    }
}
