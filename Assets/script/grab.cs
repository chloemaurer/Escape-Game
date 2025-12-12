using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private string itemName = "Diamond"; // nom de l'objet
    [SerializeField] private GameObject uiIndicator;      // indicateur UI en haut à droite
    [SerializeField] private AudioClip shinningSound;     // son joué lors de la récupération

    private bool isPickedUp = false; // indique si l'objet a déjà été ramassé

    private void OnMouseDown()
    {
        if (isPickedUp) return;

        isPickedUp = true;

        // joue le son de brillance
        PlaySound2D(shinningSound, 1f);

        // met à jour l'inventaire
        PlayerInventory.HasDiamond = true;

        // active l'indicateur UI
        if (uiIndicator != null)
            uiIndicator.SetActive(true);

        // désactive l'objet dans la scène
        gameObject.SetActive(false);

        Debug.Log("Diamant récupéré : " + itemName);
    }

    // joue un son 2D à volume donné
    private void PlaySound2D(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        GameObject temp = new GameObject("TempAudio");
        AudioSource a = temp.AddComponent<AudioSource>();
        a.clip = clip;
        a.volume = volume;
        a.spatialBlend = 0f; // son en 2D
        a.Play();
        Destroy(temp, clip.length);
    }
}
