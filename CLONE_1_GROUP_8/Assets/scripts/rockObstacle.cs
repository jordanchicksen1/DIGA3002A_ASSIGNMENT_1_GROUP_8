using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockObstacle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RockProjectile")
        {
            Destroy(gameObject, 0.5f);
            Destroy(other.gameObject);
        }

        if(other.tag == "FireProjectile")
        {
            Destroy(other.gameObject);
        }
    }
}
