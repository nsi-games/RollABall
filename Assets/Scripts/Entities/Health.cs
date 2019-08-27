using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    public int maxHealth = 100;

    [SyncVar] private int currentHealth = 0;

    public Transform healthBarPoint;

    private Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = UIManager.CreateHealthBarUI();
    }

    void OnDestroy()
    {
        if (healthBar)
        {
            Destroy(healthBar.gameObject);
        }
    }

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        // Update position of Health Bar to Health Bar Point
        healthBar.transform.position = Camera.main.WorldToScreenPoint(healthBarPoint.position);
        healthBar.value = (float)currentHealth / (float)maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
