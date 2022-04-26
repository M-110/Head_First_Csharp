using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class OneBallBehavior : MonoBehaviour
{
    [SerializeField] float xRotation;
    [SerializeField] float yRotation = 1f;
    [SerializeField] float zRotation;
    [SerializeField] float degreesPerSecond = 180f;

    [SerializeField] public int ballNumber;
    // Start is called before the first frame update

    void Start()
    {
        transform.position = new Vector3(3 - Random.value * 6, 3 - Random.value * 6, 3 - Random.value * 6);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axis = new(xRotation, yRotation, zRotation);
        transform.RotateAround(Vector3.zero, axis, degreesPerSecond * Time.deltaTime);
    }
}
