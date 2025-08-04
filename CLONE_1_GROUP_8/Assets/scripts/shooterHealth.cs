using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterHealth : MonoBehaviour
{
    public float maxHealth = 40f;
    public float currentHealth;
    public GameObject shooterWhole;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        if(currentHealth == 0f)
        {
            Destroy(shooterWhole);
        }
    }

    public void HitByFire()
    {
        currentHealth = currentHealth - 10f;
    }

    public void HitByRock()
    {
        currentHealth = currentHealth - 40f;
    }
}
