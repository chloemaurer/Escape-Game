using UnityEngine;
using UnityEngine.SceneManagement;

public class labyrintheManager : MonoBehaviour
{
    [SerializeField] private Collider[] balls; // toutes les billes à vérifier
    [SerializeField] private Collider[] zones; // zones où les billes doivent être

    void Update()
    {
        // vérifie que chaque bille est dans au moins une zone
        bool allBallsInside = true;

        foreach (Collider ball in balls)
        {
            bool thisBallInside = false;

            foreach (Collider zone in zones)
            {
                if (zone.bounds.Contains(ball.transform.position))
                {
                    thisBallInside = true;
                    break; // cette bille est déjà dans une zone
                }
            }

            if (!thisBallInside)
            {
                allBallsInside = false;
                break; // une bille en dehors, on arrête la vérification
            }
        }

        if (allBallsInside)
        {
            Debug.Log("Toutes les billes sont dans une zone !");
            OpenTrap();
        }
    }

    private void OpenTrap()
    {
        // marque la porte comme ouverte et change de scène
        PlayerPrefs.SetInt("labyrintheDoorOpened", 1);
        SceneManager.LoadScene("Escape Game");
    }
}
