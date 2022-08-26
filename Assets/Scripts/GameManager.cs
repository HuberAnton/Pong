using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Paddles
    //[SerializeField]
    //private Paddle leftPaddle;
    //[SerializeField]
    //private Paddle rightPaddle;
    [SerializeField]
    List<Paddle> paddles = new List<Paddle>();

    // Balls
    [SerializeField]
    private Ball ball;

    // Goals
    [SerializeField]
    private Goal leftGoal;
    [SerializeField]
    private Goal rightGoal;

    // Score Ui
    [SerializeField]
    private int leftPlayerScoreValue = 0;
    [SerializeField]
    private TMP_Text leftPlayerScoreText;
    [SerializeField]
    private int rightPlayerScoreValue = 0;
    [SerializeField]
    private TMP_Text rightPlayerScoreText;

    // Menu Ui
    [SerializeField]
    private GameObject menuUi;

    // Game over Ui
    [SerializeField]
    private GameObject gameoverUi;

    [SerializeField]
    // Value to be set in from outside.
    private int maxScore = 7;

    public void Start()
    {
        SetState(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SetState(3);
        }
    }



    public void SetState(int a_state)
    {
        switch (a_state)
        {
            case 0:         // Menu state
                DisableUi();
                menuUi.gameObject.SetActive(true);
                InitializeGame(0, 999999);
                break;
            case 1:         // 1 Player game
                DisableUi();
                InitializeGame(1, 7); 

                break;
            case 2:         // 2 Player game
                DisableUi();
                InitializeGame(2, 7);
                break;
            case 3:         // Close game
                Application.Quit();
                break;
            case 4:         // Game over state
                menuUi.SetActive(true);
                gameoverUi.SetActive(true);
                InitializeGame(0, 999999);
                break;
            default:
                Debug.LogError("State " + a_state + " is not currently implemented./nCheck GameManger.SetState.");
                break;
        }
    }


    private void InitializeGame(int a_activePlayers, int a_maxScore)
    {
        // Set players to start positions and apply control schemes. Player or Ai.
        for (int i = 0; i < paddles.Count; i++)
        {
            if (a_activePlayers > 0)
            {
                paddles[i].AIControlled = false;
                a_activePlayers--;
            }
            else
                paddles[i].AIControlled = true;
        }

        // Reset current scores.
        ResetScore();
        // Set maxScore
        maxScore = a_maxScore;
        // Place peices in positions.
        ResetGameBoard();
    }

    public void ResetGameBoard()
    {
        for (int i = 0; i < paddles.Count; i++)
        {
            paddles[i].ResetPosition();
        }

        ball.ResetPosition();

        ball.AddRandomStartForce();
    }

    private void ResetScore()
    {
        leftPlayerScoreValue = 0;
        leftPlayerScoreText.text = leftPlayerScoreValue.ToString();
        rightPlayerScoreValue = 0;
        rightPlayerScoreText.text = rightPlayerScoreValue.ToString();
    }


    // Both are the same should consider passing an int storing the 
    // scores and their texts in a dicitonary
    public void LefPlayerGoalScored()
    {
        leftPlayerScoreValue++;
        leftPlayerScoreText.text = leftPlayerScoreValue.ToString();

        if (leftPlayerScoreValue >= maxScore)
        {
            gameoverUi.GetComponentInChildren<TMP_Text>().text = "Left player Victory";
            SetState(4);
        }
    }

    public void RightPlayerGoalScored()
    {
        rightPlayerScoreValue++;
        rightPlayerScoreText.text = rightPlayerScoreValue.ToString();

        if(rightPlayerScoreValue >= maxScore)
        {
            gameoverUi.GetComponentInChildren<TMP_Text>().text = "Right player Victory";
            SetState(4);
        }
    }


    private void DisableUi()
    {
        menuUi.SetActive(false);
        gameoverUi.SetActive(false);
    }
}
