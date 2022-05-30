using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject oneBallPrefab;
    [SerializeField] int score;
    public bool gameOver;
    [SerializeField] int numberOfBalls;
    [SerializeField] int maxNumberOfBalls;
    
    int ballNumber;
    void Start()
    {
        InvokeRepeating(nameof(AddABall), 1.5f, 1);
    }

    void Update()
    {
        gameOver = numberOfBalls >= maxNumberOfBalls;
    }

    void OnGUI()
    {
        if (gameOver && GUILayout.Button("Restart Game"))
            RestartGame();
    }

    void AddABall()
    {
        if (gameOver) return;
        var ball = Instantiate(oneBallPrefab).GetComponent<BallBehavior>();
        ball.ballNumber = ballNumber++;
        ball.gameController = this;
        numberOfBalls++;
    }

    void RestartGame()
    {
        numberOfBalls = 0;
        foreach(var ball in GameObject.FindGameObjectsWithTag("Ball"))
            Destroy(ball);
        score = 0;
        gameOver = false;
    }

    public void ClickedOnBall()
    {
        score++;
        numberOfBalls--;
    }
}
