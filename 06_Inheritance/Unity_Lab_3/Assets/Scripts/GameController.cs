using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject oneBallPrefab;
    int ballNumber;
    void Start()
    {
        InvokeRepeating(nameof(AddABall), 1.5f, 1);
    }

    void AddABall()
    {
        Instantiate(oneBallPrefab).GetComponent<OneBallBehavior>().ballNumber = ballNumber++;
    }
}
