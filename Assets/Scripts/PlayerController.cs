using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    TargetingTriangle targetingTriangle;
    public GameObject powerInput;
    public GameObject preFlightUI;
    public GameObject flightUI;

    Rigidbody2D rb;
    public TMPro.TMP_Text altimeterText;

    public float powerMultiplier = 100f;
    public bool isOnGround = true;
    void Start()
    {
        targetingTriangle = transform.Find("TargetingTriangle").GetComponent<TargetingTriangle>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire()
    {
        Debug.Log("Fire!");
        // Implement firing logic here
        Vector3 direction = targetingTriangle.transform.position - transform.position;
        direction.z = 0; // Ensure we're working in 2D plane
        direction.Normalize();
        float power = powerInput.GetComponent<PowerBar>().GetPower();
        // Use direction and power to launch player
        Debug.Log($"Direction: {direction}, Power: {power}");
        rb.AddForce(direction * power * powerMultiplier, ForceMode2D.Impulse);

        targetingTriangle.GetComponent<SpriteRenderer>().enabled = false;
        powerInput.gameObject.SetActive(false);
        preFlightUI.SetActive(false);
        flightUI.SetActive(true);
    }

    public void Update()
    {
        UpdateAltimeter();
    }

    public void UpdateAltimeter()
    {
        if (altimeterText == null){return;}
        altimeterText.text = $"Height: {transform.position.y:F1} m\n";
        altimeterText.text += $"Distance: {transform.position.x:F1} m \n";
        altimeterText.text += $"Velocity: {rb.linearVelocity.magnitude:F1} m/s";
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
