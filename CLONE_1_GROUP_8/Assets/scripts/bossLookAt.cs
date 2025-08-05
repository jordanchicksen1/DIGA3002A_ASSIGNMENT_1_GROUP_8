using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossLookAt : MonoBehaviour
{
    public Transform player;
    public bool isInBossRange = false;
    public bossHealth bossHealth;

    //movetowards
    public bool pointA = false;
    public bool pointB = false;
    public bool pointC = false;
    public bool pointD = false;
    public Transform stayingPoint;
    public Transform pA;
    public Transform pB;
    public Transform pC;
    public Transform pD;
    public float bossSpeed;
    void Update()
    {
        if (isInBossRange == true && pointA == false && pointB == false && pointC == false && pointD == false)
        {
            this.gameObject.transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, pA.transform.position, bossSpeed * Time.deltaTime);
        }

        if (isInBossRange == true && pointA == true && pointB == false && pointC == false && pointD == false)
        {
            this.gameObject.transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, pB.transform.position, bossSpeed * Time.deltaTime);
        }

        if (isInBossRange == true && pointA == false && pointB == true && pointC == false && pointD == false)
        {
            this.gameObject.transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, pC.transform.position, bossSpeed * Time.deltaTime);
        }

        if (isInBossRange == true && pointA == false && pointB == false && pointC == true && pointD == false)
        {
            this.gameObject.transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, pD.transform.position, bossSpeed * Time.deltaTime);
        }

        if (isInBossRange == true && pointA == false && pointB == false && pointC == false && pointD == true)
        {
            this.gameObject.transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, pA.transform.position, bossSpeed * Time.deltaTime);
        }

        if (isInBossRange == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, stayingPoint.transform.position, bossSpeed * Time.deltaTime);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireProjectile")
        {
            bossHealth.HitByFire();
            Destroy(other.gameObject);
        }

        if(other.tag == "RockProjectile")
        {
            bossHealth.HitByRock();
            Destroy(other.gameObject);
        }

        if(other.tag == "PointA")
        {
            pointA = true;
            pointB = false;
            pointC = false;
            pointD = false;
            Debug.Log("is at pointA");
        }

        if (other.tag == "PointB")
        {
            pointA = false;
            pointB = true;
            pointC = false;
            pointD = false;
            Debug.Log("is at pointB");
        }

        if (other.tag == "PointC")
        {
            pointA = false;
            pointB = false;
            pointC = true;
            pointD = false;
            Debug.Log("is at pointC");
        }

        if (other.tag == "PointD")
        {
            pointA = false;
            pointB = false;
            pointC = false;
            pointD = true;
            Debug.Log("is at pointD");
        }

    }

    public void DoubleStats()
    {
        bossSpeed = bossSpeed * 1.2f;
    }
}
