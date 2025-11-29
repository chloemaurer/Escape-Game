using UnityEngine;
using UnityEngine.SceneManagement;

public class labyrintheManager : MonoBehaviour
{
    public Collider[] balls;
    public Collider[] zones;

    void Update()
    {
        // Vérifie si CHAQUE bille est dans AU MOINS UNE zone
        bool allBallsInside = true;

        foreach (Collider ball in balls)
        {
            bool thisBallInside = false;

            foreach (Collider zone in zones)
            {
                if (zone.bounds.Contains(ball.transform.position))
                {
                    thisBallInside = true;
                    break; // pas besoin de vérifier les autres zones
                }
            }

            if (!thisBallInside)
            {
                allBallsInside = false;
                break; // une seule bille dehors = on arrête ici
            }
        }

        if (allBallsInside)
        {
            Debug.Log("🎉 Les trois billes sont dans une zone !");
            OpenTrap();
        }
    }

    private void OpenTrap()
    {
        PlayerPrefs.SetInt("labyrintheDoorOpened", 1);
        SceneManager.LoadScene("Escape Game");

    }
}
