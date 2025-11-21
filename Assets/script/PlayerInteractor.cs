using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Raycast settings")]
    public Camera playerCamera;       // Caméra du joueur
    public float interactDistance = 3f; // Distance max pour interagir

    void Update()
    {
        if (playerCamera == null)
            return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // Vérifie si l’objet a un script Bell
            Bell bell = hit.collider.GetComponent<Bell>();
            if (bell != null)
            {
                // Clic gauche pour interagir
                if (Input.GetMouseButtonDown(0))
                {
                    bell.Ring();
                }

                // Option : feedback visuel (changer la couleur, etc.)
                // Renderer rend = hit.collider.GetComponent<Renderer>();
                // if (rend != null) rend.material.color = Color.yellow;
            }
        }
    }
}
