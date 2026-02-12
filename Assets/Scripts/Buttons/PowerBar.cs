using UnityEngine;

public class PowerBar: MonoBehaviour
{
    public float powerChargeRate = 1f;
    public float minimumPower = 0.1f; // Minimum power level to ensure some movement
    public float maximumPower = 1f; // Maximum power level to cap the power
    private float direction = 1f; // 1 for increasing, -1 for decreasing

    public void Awake()
    {
        // Initialize the power bar to the minimum power level
        transform.localScale = new Vector3(minimumPower, transform.localScale.y, transform.localScale.z);
    }

    public void Update()
    {
        transform.localScale += new Vector3(powerChargeRate * Time.deltaTime * direction, 0, 0);
        if (transform.localScale.x >= maximumPower)
        {
            direction = -1f; // Start decreasing
        }
        else if (transform.localScale.x <= minimumPower)
        {
            direction = 1f; // Start increasing
        }
    }

    // Returns the current power level based on the x scale of the power bar
    // Should be between minimumPower and maximumPower
    public float GetPower()
    {
        return transform.localScale.x;
    }
}
