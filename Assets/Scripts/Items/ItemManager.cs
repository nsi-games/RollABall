using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public UnityEvent onEmpty;

    // Count of how many items have been spawned
    private int itemCount = 0;

    // Use this for initialization
    void Start()
    {
        SpawnItems();
    }

    // Update is called once per frame
    void SpawnItems()
    {
        // Get every point in children
        Transform[] spawnPoints = GetComponentsInChildren<Transform>();
        // Loop through all points (Except for first)
        for (int i = 1; i < spawnPoints.Length; i++)
        {
            // Get spawn point
            Transform point = spawnPoints[i];
            // Spawn item at position
            GameObject clone = Instantiate(itemPrefab, point.position, point.rotation, transform);
            // Get item script
            Item item = clone.GetComponent<Item>();
            // Subscribe item collect to also check empty
            item.onCollect.AddListener(ItemCollected);
            // Spawn item on point
            Destroy(point.gameObject);
            // Count up item count
            itemCount++;
        }
    }

    void ItemCollected()
    {
        // Count down items
        itemCount--;
        // If there is no more items
        if(itemCount <= 0)
        {
            // Run onEmpty event
            onEmpty.Invoke();
        }
    }
}
