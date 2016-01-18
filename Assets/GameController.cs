using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    public GameObject scoreTextobj;
    public GameObject restartButtonobj;
    public GameObject gameoverTextobj;

    public float score;

    public GameObject ball;

    private Text gameoverText;
    private Button restartButton;
    private Text scoretext;

    public bool gameOver = false;
    private bool restart = false;

    void Start()
    {
        print("gamecongtroller start");
        
        score = 0f;
        scoretext = scoreTextobj.GetComponent<Text>();
        restartButton = restartButtonobj.GetComponent<Button>();
        gameoverText = gameoverTextobj.GetComponent<Text>();

        restartButtonobj.SetActive(false);
        gameoverText.text = "";

    }

    void Update()
    {
        SetCountText();
        if (gameOver)
        {
            ball.GetComponent<ball>().rb.position = new Vector2(0, 0);
            gameoverText.text =  "Gameover";
            restartButtonobj.SetActive(true);
        }
        if (restartButton)
        {
            restartButtonobj.SetActive(false);
            gameoverText.text = "";
            score = 0f;
            gameOver = false;
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
}
