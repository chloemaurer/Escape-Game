using UnityEngine;
using UnityEngine.Audio;

public class lionStatue : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition = new Vector3(-52.6003494f, 125.303947f, 827.099976f); // position finale du diamant
    [SerializeField] private Quaternion targetRotation = new Quaternion(-0.515389442f, 0.48412168f, -0.48412174f, 0.515389204f); // rotation finale du diamant
    [SerializeField] private Vector3 targetScale = new Vector3(113126.281f, 88479.4609f, 113126.281f); // échelle finale du diamant
    [SerializeField] private GameObject diamondPrefab; // prefab du diamant à placer
    [SerializeField] private AudioClip LionDoor; // son joué lors de l’ouverture

    private Animator lionhead; // référence à l’Animator de la tête de lion
    private AudioSource audioSource;
    private bool placed = false; // indique si le diamant a déjà été placé

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
        // si le joueur n’a pas le diamant ou s’il a déjà été placé → on ne fait rien
        if (!PlayerInventory.HasDiamond || placed)
            return;

        placed = true;
        PlayerInventory.HasDiamond = false;

        // désactive l’indicateur UI si présent
        GameObject indicator = GameObject.Find("DiamondIndicatorUI");
        if (indicator != null)
            indicator.SetActive(false);

        // instancie le diamant
        GameObject diamond = Instantiate(diamondPrefab);
        diamond.SetActive(true);

        // le mettre en enfant de l’Animator pour suivre la tête du lion
        diamond.transform.SetParent(lionhead.transform, false);

        // applique position, rotation et scale
        diamond.transform.position = targetPosition;
        diamond.transform.rotation = targetRotation;
        diamond.transform.localScale = targetScale;

        // déclenche l’animation d’ouverture
        lionhead.SetTrigger("openlion");

        // joue le son de la porte
        PlaySound2D(LionDoor, 1f);

        Debug.Log("Diamant placé sur la statue !");
    }

    // joue un son 2D avec un volume donné
    private void PlaySound2D(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        GameObject temp = new GameObject("TempAudio");
        AudioSource a = temp.AddComponent<AudioSource>();
        a.clip = clip;
        a.volume = volume;
        a.spatialBlend = 0f; // 0 = 2D
        a.Play();
        Destroy(temp, clip.length);
    }
}
