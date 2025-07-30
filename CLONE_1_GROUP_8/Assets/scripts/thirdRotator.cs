using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdRotator : MonoBehaviour
{
    void Update()
    {
        float rotateSpeed = 45f * Time.deltaTime;
        this.transform.Rotate(rotateSpeed, 0f, 0f);
        Debug.Log("is it working?");
    }
}
