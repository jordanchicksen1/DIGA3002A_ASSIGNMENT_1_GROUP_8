using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float shootTime;
    public float bulletSpeed;
    public enemyLookAt enemyLookAt;
   
    public void Update()
    {
        shootTime += Time.deltaTime;

        if (shootTime > 3 && enemyLookAt.isInShootingRange == true)
        {
            shootTime = 0;
            var projectile = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);

            var rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = spawnPoint.forward * bulletSpeed;

            Destroy(projectile, 2f);
        }
    }
}
