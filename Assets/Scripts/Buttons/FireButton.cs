using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player;
    public GameObject powerBar;
    public void OnPointerDown(PointerEventData eventData)
    {
        powerBar.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.GetComponent<PlayerController>().Fire();
    }
}
