using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SprayButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject sprayObject;
    public void OnPointerDown(PointerEventData eventData)
    {
        sprayObject.SetActive(true);
        Debug.Log("Spray!");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        sprayObject.SetActive(false);
        Debug.Log("Stop Spray!");
    }
}
