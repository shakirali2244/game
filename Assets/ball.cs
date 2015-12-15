using System;
using UnityEngine;

//This script manages the player object
public class ball : MonoBehaviour
{
    private Rigidbody2D rb;
    RandomProportional ran;
    private Base firstbase;
    public GameObject firstbaseobj;
    public GameObject referencebaseobj;
    public System.Collections.Generic.List<GameObject> basesobj;
    public int lastcollision;
    // Use this for initialization
    void Start()
    {
        lastcollision = 0;
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 10f;
        ran = new RandomProportional();
        firstbaseobj = GameObject.Find("GrassSprite (3)");
        referencebaseobj = GameObject.Find("GrassSprite (4)");
        basesobj.Add(firstbaseobj);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Hat" && !coll.gameObject.GetComponent<Base>().collided)
        {
           
            print("collision");
            rb.velocity = Vector2.zero;
            for (int i = 0; i < 10; i++)
            {
                rb.AddForce(Vector2.up * 1000f);
            }
            coll.gameObject.GetComponent<Base>().collided = true;
            int test = (int)(ran.getIt() * 10) - 10;
            print("test "+ test);
            createBase(new Vector2(test, rb.position.y+20));

        }

    }


    // Update is called once per frame
    void Update()
    {
        lastcollision++;
        rb.AddForce(new Vector2(Input.acceleration.x, 0) * 1000f);
        if (rb.position.x > 51)
            rb.position = new Vector2(-50, rb.position.y);
        if (rb.position.x < -51)
            rb.position = new Vector2(50, rb.position.y);
        foreach (GameObject a in basesobj)
        {
            if (a != null) {
                //print("base loc " + a.GetComponent<Base>().getXPosition() + a.GetComponent<Base>().getYPosition());
               // print("bc off? " + a.GetComponent<Base>().bc.isTrigger);
                if ((int)rb.velocity.y > 0)
                {
                    //print("velocity is " + rb.velocity.y);
                    a.GetComponent<Base>().moveY(-rb.velocity.y);
                }
                if (a.GetComponent<Base>().getYPosition() < -25)
                {
                    Destroy(a);
                }
                if (a.GetComponent<Base>().getYPosition() < rb.position.y)
                {
                    //print("BCON");
                    a.GetComponent<Base>().bcOn();
                }
            }
        }
    }

    public void createBase(Vector2 pos)
    {
        GameObject temp = Instantiate(referencebaseobj, pos, Quaternion.identity) as GameObject;
        basesobj.Add(temp);
    }


    public class RandomProportional : System.Random
    {
        public double getIt()
        {
            return base.Sample();
        }
    }
}