using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public int score = 1;
    public GameObject particlePrefab;
    public UnityEvent onCollect;
    public void SpawnParticles()
    {
        Instantiate(particlePrefab, transform.position, transform.rotation);
    }
    public void AddScore()
    {
        // Give player a score
        GameManager.Instance.AddScore(score);
    }
    public void Collect()
    {
        // Run collect event
        onCollect.Invoke();
        // Destroy item
        Destroy(gameObject);
    }
}
