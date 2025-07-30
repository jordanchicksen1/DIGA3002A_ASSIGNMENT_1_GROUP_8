using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterObstacle : MonoBehaviour
{

   
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WaterShield")
        {
            Destroy(gameObject);
        }
    }
}
