using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script manages the player object
public class ball : MonoBehaviour
{
    private Rigidbody2D rb;
    RandomProportional ran;
    public GameObject firstbaseobj;
    public GameObject referencebaseobj;
    public List<GameObject> basesobj;
    float width;
    float leftBorder;
    float rightBorder;
    GameObject scoretextobj;
    Text scoretext;
    private float score;
    private int BALL_ADJUST = 2;
    // Use this for initialization
    void Start()
    {
        score = 0f;
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 30f;
        ran = new RandomProportional();
        firstbaseobj = GameObject.Find("GrassSprite (3)");
        referencebaseobj = GameObject.Find("GrassSprite (4)");
        scoretextobj = GameObject.Find("Score");
        scoretext = scoretextobj.GetComponent<Text>();
        basesobj.Add(firstbaseobj);

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        leftBorder = -width / 2f;
        rightBorder = width / 2f;
        print(leftBorder + " rb");
        print(leftBorder + " lb");

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Hat" && !coll.gameObject.GetComponent<Base>().collided)
        {
           
            print("collision");
            rb.velocity = Vector2.zero;
            for (int i = 0; i < 10; i++)
            {
                rb.AddForce(Vector2.up * 70*rb.mass);
            }
            coll.gameObject.GetComponent<Base>().collided = true;
            int test = (int)((ran.getIt() * width) - width/2);
            createBase(new Vector2(test, rb.position.y + 15));

        }

    }


    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(new Vector2(Input.mousePosition.x-(Screen.width / 2), 0) * 3);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(100, 0) * 5);
            print("applying units of force in x axis 300");
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-100, 0) * 5);
            print("applying units of force in x axis -100");
        }
        
        print("ball height is "+ rb.position.y);
        if (rb.position.x > rightBorder)
        {
            rb.MovePosition(new Vector2(leftBorder + 1, rb.position.y));
            //print("teleporting from rt to lf");
        }
            
        if (rb.position.x < leftBorder)
        {
            //print("teleporting from rt to lf");
            rb.MovePosition(new Vector2(rightBorder - 1, rb.position.y));
        }

        moveBases();

        baseHelper();

    }

    private void createBase(Vector2 pos)
    {
        GameObject temp = Instantiate(referencebaseobj, pos, Quaternion.identity) as GameObject;
        basesobj.Add(temp);
    }

    private void moveBases()
    {
        foreach (GameObject a in basesobj)
        {
            if (a != null)
            {
                //print("base loc " + a.GetComponent<Base>().getXPosition() + a.GetComponent<Base>().getYPosition());
                // print("bc off? " + a.GetComponent<Base>().bc.isTrigger);
                if ((int)rb.velocity.y > 0)
                {
                    score++;
                    SetCountText();
                    //print("velocity is " + rb.velocity.y);
                    if (rb.position.y > 0)
                    {
                        a.GetComponent<Base>().moveY(-rb.velocity.y * BALL_ADJUST);
                        rb.velocity.Set(rb.velocity.x, 0);
                    }

                    else
                        a.GetComponent<Base>().moveY(-rb.velocity.y);


                }
                if (a.GetComponent<Base>().getYPosition() < -25)
                {
                    Destroy(a);
                }
                if (a.GetComponent<Base>().getYPosition() + a.GetComponent<Base>().getThickness() < rb.position.y)
                {
                    //print("BCON");
                    a.GetComponent<Base>().bcOn();
                }
            }
        }
    }

    private void baseHelper()
    {
        if ((int)rb.velocity.y > 0)
        {
            score++;
            SetCountText();
            int test = (int)(ran.getIt() * 10) - 10;
            if ((int)(ran.getIt() * 50) == 25)
            {
                print("spawning new platform at x= " + test);
                createBase(new Vector2(test, rb.position.y + test));
            }

            if ((int)rb.position.y > 20)
            {
                rb.AddForce(new Vector2(0, -rb.mass * rb.velocity.y));
            }
        }
    }

    void SetCountText()
    {
        scoretext.text = "Score: " + score.ToString();
    }



    public class RandomProportional : System.Random
    {
        public double getIt()
        {
            return base.Sample();
        }
    }


}