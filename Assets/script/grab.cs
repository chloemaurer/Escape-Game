using UnityEngine;

public class grab : MonoBehaviour
{
    public string itemName = "Diamond";
    public GameObject uiIndicator; // L’indicateur en haut à droite

    private bool isPickedUp = false;

    private void OnMouseDown()
    {
        if (isPickedUp) return;

        // Marquer comme récupéré
        isPickedUp = true;

        // Cacher le diamant dans la scène
        gameObject.SetActive(false);

        // Dire au système qu'on possède le diamant
        PlayerInventory.HasDiamond = true;

        // Activer l’indicatif UI
        if (uiIndicator != null)
            uiIndicator.SetActive(true);

        Debug.Log("💎 Diamant récupéré !");
    }
}
