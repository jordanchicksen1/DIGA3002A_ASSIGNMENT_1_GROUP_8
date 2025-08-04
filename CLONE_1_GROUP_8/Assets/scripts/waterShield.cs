using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterShield : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
        }
    }
}
