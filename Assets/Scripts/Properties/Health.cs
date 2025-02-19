using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public bool isPlayer;
    public delegate void HealthChangeHandler(object source, int oldHealth, int newHealth);
    public event HealthChangeHandler OnHealthChanged;
    public int CurrentHealth => currentHealth;
    private HealthManager gameManager;
    public void changeHealth(int amount)
    {
        if (isPlayer)
        {
            gameManager.PlayerHealth += amount;
            currentHealth = gameManager.PlayerHealth;
        }
        else
        {
            int oldHealth = currentHealth;
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            OnHealthChanged?.Invoke(this, oldHealth, currentHealth);
            if (currentHealth <=0)
            {
                Destroy(this.gameObject);
            }
        }

    }
    public int GetHealth()
    {
        return currentHealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (isPlayer)
        {
            gameManager = GameManager.instance.gameObject.GetComponent<HealthManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
