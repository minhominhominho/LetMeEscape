using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWall : MonoBehaviour
{
    private const float rotatingSpeed = 35;


    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotatingSpeed) * Time.deltaTime);
    }
}
