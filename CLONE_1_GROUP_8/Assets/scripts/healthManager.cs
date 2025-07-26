using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class healthManager : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    public GameObject gameOverScreen;



    public void Start()
    {
        currentHealth = maxHealth;
        updateHealthBar();
    }

    public void Update()
    {
        if (currentHealth == 0)
        {
            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void updateHealth(float amount)
    {
        currentHealth += amount;
        updateHealthBar();

    }

    public void updateHealthBar()
    {
        float targetFillAmount = currentHealth / maxHealth;
        healthBar.fillAmount = targetFillAmount;
    }

    [ContextMenu("PlayerHit")]
    public void PlayerHit()
    {
        currentHealth = currentHealth - 10f;
        updateHealthBar();
       

    }

    [ContextMenu("PlayerHeal")]
    public void PlayerHeal()
    {
        currentHealth = currentHealth + 50f;
        updateHealthBar();
        

    }
}
