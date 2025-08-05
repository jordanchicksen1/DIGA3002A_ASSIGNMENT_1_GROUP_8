using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLookAt : MonoBehaviour
{
    public Transform player;
    public bool isInShootingRange = false;
    public shooterHealth shooterHealth;
    void Update()
    {
        if (isInShootingRange == true)
        {
            this.gameObject.transform.LookAt(player);
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FireProjectile")
        {
            shooterHealth.HitByFire();
            Destroy(other.gameObject);
            Debug.Log("hit by fireball");
        }

        if(other.tag == "RockProjectile")
        {
            shooterHealth.HitByRock();
            Destroy(other.gameObject);
            Debug.Log("Hit by rock");
        }
    }
}
