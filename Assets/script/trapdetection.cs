using UnityEngine;

public class trapdetection : MonoBehaviour
{

    public TrapAnimation trap;   // référence à la porte
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Quelque chose est entré : " + other.name);

        if (other.CompareTag(playerTag))
        {
            Debug.Log("C'est le player !");
            trap.CloseDoor();    // <<< Fermeture de la porte
        }
    }
}
