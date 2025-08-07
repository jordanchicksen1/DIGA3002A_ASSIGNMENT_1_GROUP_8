using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionInstantiator : MonoBehaviour
{
    public GameObject potion;
    public Transform spawnPoint;
    public float spawnTime;
    public float spawnRecoveryTime = 30;
    public bossLookAt bossLookAt;

    public void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime > spawnRecoveryTime && bossLookAt.isInBossRange == true)
        {
            spawnTime = 0;
            var projectile = Instantiate(potion, spawnPoint.position, spawnPoint.rotation);

            Destroy(projectile, 30f);
        }
    }
}
