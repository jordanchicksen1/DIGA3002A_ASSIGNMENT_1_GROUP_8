using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class bossHealth : MonoBehaviour
{
    public float maxHealth = 1000f;
    public float currentHealth;
    public Image bossHealthBar;
    public GameObject bossWhole;

    public bossShooter bossShooter;
    public bossLookAt bossLookAt;

    public bool statsHaveDoubled = false;

    public void Start()
    {
        currentHealth = maxHealth;
        updateBossHealthBar();
    }

    public void Update()
    {
        if (currentHealth <= 0f)
        {
            Destroy(bossWhole);
        }

        if(currentHealth <= 500f && statsHaveDoubled == false)
        {
            DoubleAllStats();
        }
    }

    public void updateBossHealthBar()
    {
        float targetFillAmount = currentHealth / maxHealth;
        bossHealthBar.fillAmount = targetFillAmount;

    }

    public void updateBossHealth(float amount)
    {
        currentHealth += amount;
        updateBossHealthBar();

    }


    public void HitByFire()
    {
        currentHealth = currentHealth - 15f;
        StartCoroutine(BossStopper());
        updateBossHealthBar();
    }

    public void HitByRock()
    {
        currentHealth = currentHealth - 20f;
        StartCoroutine(BossStopper());
        updateBossHealthBar();

    }

    public void FullHealBoss()
    {
        currentHealth = maxHealth;
        updateBossHealthBar();
    }

    public void DoubleAllStats()
    {
        statsHaveDoubled = true;
        bossLookAt.DoubleStats();
        bossShooter.DoubleStats();
    }

    public IEnumerator BossStopper()
    {
        yield return new WaitForSeconds(0f);
        bossLookAt.bossSpeed = bossLookAt.bossSpeed - 4f;
        yield return new WaitForSeconds(0.5f);
        bossLookAt.bossSpeed = bossLookAt.bossSpeed + 4f;

    }
}
