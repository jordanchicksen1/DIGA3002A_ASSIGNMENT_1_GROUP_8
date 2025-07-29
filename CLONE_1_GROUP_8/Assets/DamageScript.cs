using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{

    [SerializeField] private float weaponHitRadius;
    [SerializeField] private int damage = 2;

    [SerializeField] private LayerMask targetLayer;
    
    void Update()
    {
        DetectHit();
    }

    private void DetectHit()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, weaponHitRadius, targetLayer);

        if (hit.Length > 0)
        {
            healthManager targethealthManager = hit[0].GetComponent<healthManager>();

            targethealthManager.TakeDamage(damage);

            gameObject.SetActive(false);
        }
        {
            
        }
    }
}
