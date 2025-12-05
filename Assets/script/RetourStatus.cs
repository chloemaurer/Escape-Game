using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RetourStatus: MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Escape Game");
    }
}
