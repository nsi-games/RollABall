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
        Transform[] points = spawnPointParent.GetComponentsInChildren<Transform>();
        for (int i = 1; i < points.Length; i++)
        {
            SpawnItem(points[i].position);
        }
    }

    void SpawnItem(Vector3 position)
    {
        GameObject item = Instantiate(itemPrefab, position, Quaternion.identity, spawnPointParent) as GameObject;
        NetworkServer.Spawn(item);
    }
}
