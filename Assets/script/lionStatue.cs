using UnityEngine;
using UnityEngine.Audio;

public class lionStatue : MonoBehaviour
{
    [Header("Position finale du diamant")]
    public Vector3 targetPosition = new Vector3(-52.6003494f, 125.303947f, 827.099976f);

    [Header("Rotation finale du diamant")]
    public Quaternion targetRotation = new Quaternion(-0.515389442f, 0.48412168f, -0.48412174f, 0.515389204f);

    [Header("Échelle finale du diamant")]
    public Vector3 targetScale = new Vector3(113126.281f, 88479.4609f, 113126.281f);

    [Header("Prefab du diamant à placer")]
    public GameObject diamondPrefab;
    private Animator lionhead;

    private bool placed = false;
    public AudioClip LionDoor;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        lionhead = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        // Si le joueur n’a pas le diamant ou si c'est déjà placé → on arrête
        if (!PlayerInventory.HasDiamond || placed)
            return;

        placed = true;
        PlayerInventory.HasDiamond = false;

        // Désactiver l'indicateur UI si trouvé
        GameObject indicator = GameObject.Find("DiamondIndicatorUI");
        if (indicator != null)
            indicator.SetActive(false);

        // Instancier le diamant
        GameObject diamond = Instantiate(diamondPrefab);

        // S'assurer que le diamant est actif
        diamond.SetActive(true);

        // Le mettre enfant du bone qui correspond à l'œil du lion
        diamond.transform.SetParent(lionhead.transform, false);

        // Appliquer position / rotation / scale
        diamond.transform.position = targetPosition;
        diamond.transform.rotation = targetRotation;
        diamond.transform.localScale = targetScale;

        lionhead.SetTrigger("openlion");
        PlaySound2D(LionDoor, 1f);
        Debug.Log("💎 Diamant placé sur la statue !");
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
