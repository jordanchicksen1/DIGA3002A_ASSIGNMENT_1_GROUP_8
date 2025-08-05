using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float shootTime;
    public float shootRecoveryTime;
    public float bulletSpeed;
    public bossLookAt bossLookAt;

    public void Update()
    {
        shootTime += Time.deltaTime;

        if (shootTime > shootRecoveryTime && bossLookAt.isInBossRange == true)
        {
            shootTime = 0;
            var projectile = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

            var rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = spawnPoint.forward * bulletSpeed;

            Destroy(projectile, 2f);
        }
    }

    public void DoubleStats()
    {
        shootRecoveryTime = shootRecoveryTime - 1;
        bulletSpeed = bulletSpeed * 1.2f;
    }
}
