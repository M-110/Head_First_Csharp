using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float xRotation;
    [SerializeField] float yRotation;
    [SerializeField] float zRotation;
    [SerializeField] float rotationSpeed = 180;

    void Update()
    {
        var axis = new Vector3(xRotation, yRotation, zRotation);
        transform.Rotate(axis, rotationSpeed * Time.deltaTime);
        Debug.DrawRay(Vector3.zero, axis, Color.magenta, 0.5f);
    }
}
