using System;
using Unity.VisualScripting;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    GameObject despawner;
    GameObject spawner;
    GameObject player;

    private float keepNearPlayerDistance = 20f;

    public virtual void Start()
    {
        despawner = Despawner.Instance.gameObject;
        spawner = Spawner.Instance.gameObject;
        player = spawner.GetComponent<Spawner>().Player;
    }
    public virtual void Update()
    {
        if (despawner == null) return;
        if (transform.position.x < despawner.transform.position.x)
        {
            spawner.GetComponent<Spawner>().ActiveSpawnedObjects.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void KeepNearPlayer()
    {
        if (despawner == null || player == null) return;
        // object is too low below player, move it up
        if (transform.position.y < player.transform.position.y - keepNearPlayerDistance)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + 0.5f * keepNearPlayerDistance, transform.position.z);
        }
        // object is too high above player, move it down
        else if (transform.position.y > player.transform.position.y + keepNearPlayerDistance)
        {
            transform.position = new Vector3(transform.position.x, Math.Max(player.transform.position.y - 0.5f * keepNearPlayerDistance, spawner.GetComponent<Spawner>().FloorYPosition + 1), transform.position.z);
        }
    }
}
