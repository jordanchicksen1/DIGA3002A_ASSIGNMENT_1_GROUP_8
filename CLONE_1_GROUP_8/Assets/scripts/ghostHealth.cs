using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostHealth : MonoBehaviour
{
    public float maxHealth = 40f;
    public float currentHealth;
    public GameObject ghostWhole;

    public AudioSource enemySounds;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void Update()
    {
        if (currentHealth == 0f)
        {
            Destroy(ghostWhole);
        }
    }

    public void HitByFire()
    {
        currentHealth = currentHealth - 40f;
        enemySounds.Play();
    }

   
}
