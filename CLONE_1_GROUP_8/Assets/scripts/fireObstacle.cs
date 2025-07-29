using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireObstacle : MonoBehaviour
{
    public float destroyTime = 1f;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FireProjectile")
        {
            Destroy(other.gameObject);
            Destroy(gameObject, destroyTime);
        }

        if(other.tag == "RockProjectile")
        {
            Destroy(other.gameObject);
        }
    }
}
