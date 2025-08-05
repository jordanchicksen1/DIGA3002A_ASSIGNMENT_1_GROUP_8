using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ghostLookAt : MonoBehaviour
{
    public Transform player;
    public bool isInGhostRange = false;
    public ghostHealth ghostHealth;

    //movetowards
    public Transform stayingPoint;
    public float ghostSpeed;
    void Update()
    {
        if (isInGhostRange == true)
        {
            this.gameObject.transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position,ghostSpeed * Time.deltaTime );
        }

        if(isInGhostRange == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, stayingPoint.transform.position ,ghostSpeed * Time.deltaTime );
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireProjectile")
        {
            ghostHealth.HitByFire();
            Destroy(other.gameObject);
        }

        
    }
}
