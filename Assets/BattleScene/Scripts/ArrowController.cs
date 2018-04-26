using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    public float speed = 6f;    //arrow speed
    
    private Rigidbody2D rb;

    private int direction;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {


        //sends the arrow in set direction
        switch (direction) {
            case (int)Direction.UP:
                rb.velocity = new Vector2(0, speed);    //MOVES UP
 
                break;
            case (int)Direction.DOWN:
                rb.velocity = new Vector2(0, -speed);    //MOVES DOWN

                break;
            case (int)Direction.LEFT:
                rb.velocity = new Vector2(-speed, 0);    //MOVES LEFT

                break;
            case (int)Direction.RIGHT:
                rb.velocity = new Vector2(speed, 0);    //MOVES RIGHT

                break;

        }

        transform.up = rb.velocity.normalized;  //sets sprite to correct facing

    }

    private void OnBecameInvisible() {

        this.gameObject.SetActive(false);   //for bullet recycling
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SetActive(false);

            this.gameObject.SetActive(false); //for bullet recycling

        }
    }

    //GETTER and SETTER for direction

    public void SetDirection(int dir) {
        direction = dir;
    }

    public int GetDirection() {
        return direction;
    }

}
