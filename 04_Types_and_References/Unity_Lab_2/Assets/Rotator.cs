using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float xRotation;
    [SerializeField] float yRotation;
    [SerializeField] float zRotation;
    [SerializeField] float rotationSpeed = 180;

    void Update()
    {
        var axis = new Vector3(xRotation, yRotation, zRotation);
        transform.RotateAround(Vector3.zero, axis, rotationSpeed * Time.deltaTime);
        Debug.DrawRay(Vector3.zero, axis, Color.green, 0.5f);
    }
}
