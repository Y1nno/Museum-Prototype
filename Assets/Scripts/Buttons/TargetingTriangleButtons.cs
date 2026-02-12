using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetingTriangleButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TargetingTriangle TargetingTriangleObject;
    public float RotationDirection = 1f; // 1 for clockwise, -1 for counter-clockwise
    private bool _isRotating = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        // Handle pointer down event
        _isRotating = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Handle pointer up event
        _isRotating = false;
    }

    void Update()
    {
        if (_isRotating && TargetingTriangleObject != null)
        {
            TargetingTriangleObject.rotateTargeter(RotationDirection);
        }
    }
}   