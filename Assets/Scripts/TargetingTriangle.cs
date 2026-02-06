using Unity.Mathematics;
using UnityEngine;


public class TargetingTriangle : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationSpeed = 90f; // degrees per second
    public float upperZLimit = 0f;
    public float lowerZLimit = -90f;

    public void rotateTargeter(float direction)
    {
        if (!pivotPoint) return;

        float rotationAmount = direction * rotationSpeed * Time.deltaTime;

        Vector3 offset = transform.position - pivotPoint.position;
        offset = Quaternion.Euler(0f, 0f, rotationAmount) * offset;

        transform.position = pivotPoint.position + offset;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, offset.normalized);
    }
}