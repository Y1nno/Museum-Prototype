using Unity.VisualScripting;
using UnityEngine;

public class BoostController : MonoBehaviour
{
    GameObject player;

    [Tooltip("The ratio of the boost applied to the player's horizontal movement, normalized with verticalBoostRatio.")]
    public float horizontalBoostRatio = 1f;
    [Tooltip("The ratio of the boost applied to the player's vertical movement, normalized with horizontalBoostRatio.")]
    public float verticalBoostRatio = 1f;
    public float boostStrength = 10f;
    public GameObject fuelBar;
    [Tooltip("The rate at which fuel is consumed while boosting, in percent per second.")]
    public float fuelConsumptionRate = 50f;
    private Vector2 boostVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boostVector = new Vector2(horizontalBoostRatio, verticalBoostRatio).normalized;
    }

    void Update()
    {
        if (CheckForFuel())
        {
            ApplyBoost();
            DecreaseFuel();
            return;
        }
        DisableBoost();
    }

    private void ApplyBoost()
    {
        player.GetComponent<Rigidbody2D>().AddForce(boostVector * boostStrength * Time.deltaTime, ForceMode2D.Force);
    }

    private void DecreaseFuel()
    {
        fuelBar.transform.localScale -= new Vector3(fuelConsumptionRate * Time.deltaTime / 100, 0, 0);
        if (fuelBar.transform.localScale.x < 0)
        {
            fuelBar.transform.localScale = new Vector3(0, fuelBar.transform.localScale.y, fuelBar.transform.localScale.z);
            DisableBoost();
        }
    }

    private bool CheckForFuel()
    {
        return fuelBar.transform.localScale.x > 0;
    }

    private void DisableBoost()
    {
        gameObject.SetActive(false);
    }
}
