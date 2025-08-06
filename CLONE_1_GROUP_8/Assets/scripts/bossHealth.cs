using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossHealth : MonoBehaviour
{
    public float maxHealth = 1000f;
    public float currentHealth;
    public Image bossHealthBar;
    public GameObject bossWhole;

    public GameObject gameFinishedScreen;

    public bossShooter bossShooter;
    public bossLookAt bossLookAt;

    public bool statsHaveDoubled = false;
    public player player;

    public ParticleSystem doubleStatsFX;
    public ParticleSystem hitFX;

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
            player.EndGame();
            
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
        hitFX.Play();
        StartCoroutine(BossStopper());
        updateBossHealthBar();
    }

    public void HitByRock()
    {
        currentHealth = currentHealth - 20f;
        hitFX.Play();
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
        doubleStatsFX.Play();
    }

    public void RevertAllStats()
    {
        statsHaveDoubled = false;
        bossLookAt.RevertStats();
        bossShooter.RevertStats();
    }

    public IEnumerator BossStopper()
    {
        yield return new WaitForSeconds(0f);
        bossLookAt.bossSpeed = bossLookAt.bossSpeed - 4f;
        yield return new WaitForSeconds(0.5f);
        bossLookAt.bossSpeed = bossLookAt.bossSpeed + 4f;

    }

   

    
}
