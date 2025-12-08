using UnityEngine;

public class Grab : MonoBehaviour
{
    [Header("Nom de l'objet")]
    public string itemName = "Diamond";

    [Header("UI Indicator")]
    public GameObject uiIndicator; // L’indicateur en haut à droite

    [Header("Son de brillance")]
    public AudioClip shinningSound;

    private bool isPickedUp = false;

    private void OnMouseDown()
    {
        if (isPickedUp) return;

        isPickedUp = true;

        PlaySound2D(shinningSound, 1f);


        // Mettre à jour l’inventaire
        PlayerInventory.HasDiamond = true;

        // Activer l’indicateur UI
        if (uiIndicator != null)
            uiIndicator.SetActive(true);

        // Cacher / désactiver le diamant
        gameObject.SetActive(false);

        Debug.Log("💎 Diamant récupéré !");
    }

    private void PlaySound2D(AudioClip clip, float volume = 1f)
    {
        GameObject temp = new GameObject("TempAudio");
        AudioSource a = temp.AddComponent<AudioSource>();
        a.clip = clip;
        a.volume = volume;
        a.spatialBlend = 0f; // 0 = 2D, 1 = 3D
        a.Play();
        Destroy(temp, clip.length);
    }

}
