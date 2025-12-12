using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;   // caméra utilisée pour le raycast
    [SerializeField] private float interactDistance = 3f; // distance max pour interagir

    void Update()
    {
        if (playerCamera == null) return;

        // lance un raycast depuis la caméra vers l’avant
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // vérifie si l’objet touché a un script Bell
            Bell bell = hit.collider.GetComponent<Bell>();
            if (bell != null)
            {
                // clic gauche → sonner la cloche
                if (Input.GetMouseButtonDown(0))
                {
                    bell.Ring();
                }

                // possibilité d’un feedback visuel si besoin
                // Renderer rend = hit.collider.GetComponent<Renderer>();
                // if (rend != null) rend.material.color = Color.yellow;
            }
        }
    }
}
