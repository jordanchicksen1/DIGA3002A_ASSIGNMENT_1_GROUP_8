using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectRotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        float rotateSpeed = 45f * Time.deltaTime;
        this.transform.Rotate(0f, 0f, rotateSpeed);
        Debug.Log("is it working?");
    }
}
