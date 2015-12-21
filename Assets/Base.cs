using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {
    Rigidbody2D rb;
    public BoxCollider2D bc;
    public bool collided = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        
    }

    public void bcOn()
    {
        bc.isTrigger = false;
    }
    public void bcOff()
    {
        bc.isTrigger = true;
    }




    // Update is called once per frame
    void Update () {
	    
	}

    public float getThickness()
    {
        return bc.size.y / 2;
    }

    public void moveY(float inp)
    {
        rb.MovePosition(rb.position + new Vector2(0, inp) * Time.fixedDeltaTime);
    }

    public void moveY(Vector2 force)
    {
        rb.AddForce(force);
    }

    public float getYPosition()
    {
        return rb.position.y;
    }
    public float getXPosition()
    {
        return rb.position.x;
    }

    public void setPosition(Vector2 inp)
    {
        rb.position = inp;
    }
}
