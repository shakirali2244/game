using UnityEngine;
using UnityEngine.UI;

//Vector Illustration by <a rel="nofollow" href="https://www.vecteezy.com">www.Vecteezy.com</a>

public class GameController : MonoBehaviour {


    public GameObject scoreTextobj;
    public GameObject restartButtonobj;
    public GameObject gameoverTextobj;

    public float score;

    public GameObject ball;
    private ball ballref;

    private Text gameoverText;
    private Button restartButton;
    private Text scoretext;

    public bool gameOver = false;
    private bool restart = false;

    void Start()
    {
        print("gamecongtroller start");
        
		score = 0f;
		scoreTextobj = GameObject.Find("Score");
		//restartButtonobj = GameObject.Find("RestartButton");
		gameoverTextobj = GameObject.Find("GameOver");
		scoretext = scoreTextobj.GetComponent<Text>();
		restartButton = restartButtonobj.GetComponent<Button>();
		gameoverText = gameoverTextobj.GetComponent<Text>();

        restartButtonobj.SetActive(false);
        gameoverText.text = "";

        ballref = ball.GetComponent<ball>();
    }

    void Update()
    {
        SetCountText();
        if (gameOver)
        {
            ballref.setYposition(0);
            ballref.rb.velocity = Vector2.zero;
            gameoverText.text =  "Gameover";
            restartButtonobj.SetActive(true);
        }
        if (restart)
        {
            ballref.setYposition(0);
            ballref.resetBases();
            restartButtonobj.SetActive(false);
            gameoverText.text = "";
            score = 0f;
            gameOver = false;
            restart = false;
        }


    }

    void SetCountText()
    {
        scoretext.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        gameoverText.text = "Game Over!";
        gameOver = true;
    }

    public void restartgame() {
        restart = true;
    }
}
