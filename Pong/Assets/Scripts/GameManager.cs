using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public void Awake() {
        if (instance == null) {
            instance = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
    }

    public GameObject ball;

    public bool isGamePaused;
    public GameObject gameOverUI;

    public Text p1ScoreText;
    public Text p2ScoreText;

    public int goalsToWin;

    private int p1Score = 0;
    private int p2Score = 0;

    // Start is called before the first frame update
    void Start() {
        isGamePaused = false;
    }

    private void Update() {
        if (!isGamePaused) {
            UpdateScoreboard();
        }
    }

    public void Score(int boundaryID) {
        if (boundaryID == 1) {
            UpdateScore(2);
        } else if (boundaryID == 2) {
            UpdateScore(1);
        }
    }

    public void UpdateScore(int playerID) {
        switch (playerID) {
            case 1:
                p1Score++;
                break;
            case 2:
                p2Score++;
                break;
        }

        if (p1Score == goalsToWin) {
            GameOver(1);
        } else if (p2Score == goalsToWin) {
            GameOver(2);
        }
    }

    public void UpdateScoreboard() {
        p1ScoreText.text = p1Score.ToString();
        p2ScoreText.text = p2Score.ToString();
    }

    public void GameOver(int winnerID) {
        isGamePaused = true;

        if (winnerID == 1) {
            p1ScoreText.text = "Winner";
        } else if (winnerID == 2) {
            p2ScoreText.text = "Winner";
        }

        ToggleGameOverUI();
    }

    public void ToggleGameOverUI() {
        gameOverUI.SetActive(!gameOverUI.activeSelf);
    }

    public void ResettGame() {
        ball.GetComponent<BallController>().ResetBall();
    }

    public void PlayAgain() {
        ResettGame();
    }

    public void MainMenu() {

    }
}
