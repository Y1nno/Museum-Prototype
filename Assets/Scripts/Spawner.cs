using System;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject FloorPrefab;
    public GameObject Player;

    public float FloorYPosition = 0f;
    public float HorizontalFloorDistance = 1f;

    public float MaxSpawnedObjects = 10;
    public float RelativeSpawnYAbove = 5f;
    public float RelativeSpawnYBelow = 1f; // positive values only

    [Tooltip("0 means no clustering, 1 means max clustering")]
    public float clusteringCoefficient = 0.5f; // 0 means no clustering, 1 means max clustering
    public List<GameObject> SpawnablePrefabs = new List<GameObject>();
    public List<GameObject> ActiveSpawnedObjects { get; private set; } = new List<GameObject>();
    public float LastFloorSpawnXPosition = 0f;

    private float spawnAheadDistance = 30f;

    public static Spawner Instance { get; private set; }
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        spawnAheadDistance = transform.position.x - Player.transform.position.x;
    }

    void Update()
    {
        UpkeepFloorSegments();
        SpawnWorldItems();
    }

    private void UpkeepFloorSegments()
    {
        if (Player == null || FloorPrefab == null) return;

        float playerX = Player.transform.position.x;

        while (LastFloorSpawnXPosition < playerX + spawnAheadDistance)
        {
            LastFloorSpawnXPosition += HorizontalFloorDistance;
            Instantiate(
                FloorPrefab,
                new Vector3(LastFloorSpawnXPosition, FloorYPosition, 0f),
                Quaternion.identity
            );
        }
    }

    public void SpawnWorldItems()
    {
        if (Player == null || SpawnablePrefabs.Count == 0 || Player.GetComponent<Rigidbody2D>().linearVelocityX == 0) return;
        if (ActiveSpawnedObjects.Count < MaxSpawnedObjects)
        {
            if (Mathf.Clamp01(clusteringCoefficient) <= UnityEngine.Random.Range(0f, 1f))
            {
                return; // Skip this spawn based on clustering coefficient
            }
            GameObject prefabToSpawn = SpawnablePrefabs[UnityEngine.Random.Range(0, SpawnablePrefabs.Count)];
            float spawnXPosition = transform.position.x + spawnAheadDistance + UnityEngine.Random.Range(0f, 10f);
            float minY = Math.Max(transform.position.y - RelativeSpawnYBelow, FloorYPosition + 1f);
            float spawnYPosition = FloorYPosition + UnityEngine.Random.Range(minY, transform.position.y + RelativeSpawnYAbove);
            GameObject spawnedObject = Instantiate(
                prefabToSpawn,
                new Vector3(spawnXPosition, spawnYPosition, 0f),
                Quaternion.identity
            );

            ActiveSpawnedObjects.Add(spawnedObject);
        }
    }
}