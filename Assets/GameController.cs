using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    GameObject scoretextobj;
    public Text scoretext;
    public float score;

    public GameObject ball;
    public GUIText gameOverText;
    public GUIText restartText;
    public bool gameOver;
    private bool restart;

    void start()
    {
        restartText.text = "";
        gameOverText.text = "";
        score = 0f;
        scoretextobj = GameObject.Find("Score");
        scoretext = scoretextobj.GetComponent<Text>();
        ball = GameObject.Find("ball");
    }

    void Update()
    {
        SetCountText();
        if (gameOver)
        {
            Destroy(ball);
            gameOverText.text =  "Gameover";
            restartText.text = "Restart Fag";
        }
    }

    void SetCountText()
    {
        scoretext.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
