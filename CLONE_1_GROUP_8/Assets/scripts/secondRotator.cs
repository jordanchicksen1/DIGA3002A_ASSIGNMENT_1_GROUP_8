using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondRotator : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        float rotateSpeed = 45f * Time.deltaTime;
        this.transform.Rotate(0f, rotateSpeed, 0f);
        Debug.Log("is it working?");
    }
}
