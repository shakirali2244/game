﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script manages the player object
public class ball : MonoBehaviour
{
    public Rigidbody2D rb;
    private RandomProportional ran;
    private GameObject firstbaseobj;
    private GameObject referencebaseobj;
    public GameObject gcref;
    private GameController gc;
    public List<GameObject> basesobj;
    float width;
    float leftBorder;
    float rightBorder;
    
    private int BALL_ADJUST = 2;
    // Use this for initialization
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 30f;
        ran = new RandomProportional();
        firstbaseobj = GameObject.Find("GrassSprite (3)");
        referencebaseobj = GameObject.Find("GrassSprite (4)");
        
        basesobj.Add(firstbaseobj);
        gc = gcref.GetComponent<GameController>();
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
        if (coll.gameObject.tag == "Hat")
        {
           
            //print("collision");
            rb.velocity = Vector2.zero;
            for (int i = 0; i < 10; i++)
            {
                rb.AddForce(Vector2.up * 70*rb.mass);
            }	
            int test = (int)((ran.getIt() * width) - width/2);
            //createBase(new Vector2(test, rb.position.y + 15));

        }

    }


    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(new Vector2(Input.mousePosition.x-(Screen.width / 2), 0) * 3);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(100, 0) * 5);
            //print("applying units of force in x axis 300");
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-100, 0) * 5);
            //print("applying units of force in x axis -100");
        }
        
        //print("ball height is "+ rb.position.y);
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

		if ((int)rb.velocity.y > 0) {
			gc.score++;
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
				Base curr = a.GetComponent<Base>();
                //print("base loc " + a.GetComponent<Base>().getXPosition() + a.GetComponent<Base>().getYPosition());
                // print("bc off? " + a.GetComponent<Base>().bc.isTrigger);
                if ((int)rb.velocity.y > 0)
                {
                    
					if ((int)rb.position.y > 10)
					{
						rb.AddForce(new Vector2(0, -rb.mass * rb.velocity.y));
					}
                    //print("velocity is " + rb.velocity.y);
                    else if (rb.position.y > 0)
                    {
						curr.moveY(-rb.velocity.y * BALL_ADJUST);
                        rb.velocity.Set(rb.velocity.x, 0);
                    }

                    else
						curr.moveY(-rb.velocity.y);


                }
				if (curr.bc.isTrigger == false && curr.getYPosition () + curr.getThickness () < rb.position.y) {
					//print("BCON");
					curr.bcOn ();
				} else if (curr.bc.isTrigger == true && curr.getYPosition () + curr.getThickness () > rb.position.y) {
					curr.bcOff ();
				}
				if (curr.getYPosition() < -25)
				{
					Destroy(a);
					basesobj.Remove(a); //remove the first one
				}
            }
        }
        if (rb.position.y < -30)
        {
            gc.gameOver = true;
        }
    }

    public void resetBases()
    {
        foreach (GameObject a in basesobj)
        {
            Destroy(a);
        }
        basesobj.Clear();
        createBase(new Vector2(0, -20));
    }

    private void baseHelper()
    {
		GameObject tmp = basesobj [basesobj.Count - 1]; //get the last added base
		float y_last = tmp.GetComponent<Base>().getYPosition();
		if (y_last < 30)
        {
            //gc.score++;
			print(y_last);

            int test = (int)(ran.getIt() * 40) - 20;
            if (true)
            {
                print("spawning new platform at x= " + test);
				createBase(new Vector2((float)((ran.getIt() * rightBorder) - rightBorder/2), y_last + 10));
            }

            
        }
    }

    public void setYposition(float y)
    {
        GetComponent<ball>().rb.position = new Vector2(0, y);
    }
    

    



    public class RandomProportional : System.Random
    {
        public double getIt()
        {
            return base.Sample();
        }
    }


}