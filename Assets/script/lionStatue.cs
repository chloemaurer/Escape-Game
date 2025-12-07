using UnityEngine;

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

    private bool placed = false;

    private void OnMouseDown()
    {
        if (!PlayerInventory.HasDiamond || placed) return;

        placed = true;
        PlayerInventory.HasDiamond = false;

        // Désactiver l’indicateur UI
        GameObject indicator = GameObject.Find("DiamondIndicatorUI");
        if (indicator != null) indicator.SetActive(false);

        // Instancier le diamant sur la statue
        GameObject diamond = Instantiate(diamondPrefab);

        // Appliquer position / rotation / scale exacts
        diamond.transform.position = targetPosition;
        diamond.transform.rotation = targetRotation;
        diamond.transform.localScale = targetScale;

        Debug.Log("💎 Diamant placé sur la statue !");
    }
}
