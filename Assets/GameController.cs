using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject ball;

    void start()
    {
        ball = GameObject.Find("ball");
    }

    void addScore()
    {

    }
}
