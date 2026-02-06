using UnityEngine;

public class Despawner : MonoBehaviour
{
    public static Despawner Instance { get; private set; }
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
