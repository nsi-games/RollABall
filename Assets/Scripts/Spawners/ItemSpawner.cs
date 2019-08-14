using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;

public class ItemSpawner : NetworkBehaviour
{
    public GameObject itemPrefab;
    public Transform spawnPointParent;

    public override void OnStartServer()
    {
        // Collect all Transform within SpawnPoint parent
        Transform[] spawnPoints = spawnPointParent.GetComponentsInChildren<Transform>();

        // Spawn an item on each point's position
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            SpawnItem(spawnPoints[i].position);
        }

        // Destroy all spawn points
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            Destroy(spawnPoints[i].gameObject);
        }
    }

    void SpawnItem(Vector3 position)
    {
        GameObject item = Instantiate(itemPrefab, position, Quaternion.identity, spawnPointParent) as GameObject;
        NetworkServer.Spawn(item);
    }
}
