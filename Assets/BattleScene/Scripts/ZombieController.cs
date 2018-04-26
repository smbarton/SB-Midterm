using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ZombieController : MonoBehaviour {

    public float speed = 0.5f;  //movement speed
    public int health = 1;      //how many hits it can take

    private int direction;  //the direction it will face

    private Rigidbody2D rb;

    public int attackSpeed = 3; //how fast it will hit a wall

    private Vector2 movement;   //moving enemy in the right direction

    private IEnumerator coroutine;  //called like this to pass an argument later

    Animator anim;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        
        SetMovement();
	}

    private void FixedUpdate() {
        rb.velocity = movement; //this is the direction the enemy goes
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {

        //start attacking the wall
        if (collision.gameObject.tag == "Wall") {
            coroutine = StartAttack(collision); //passing argument to coroutine
            StartCoroutine(coroutine);  //calling that coroutine
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        
        //When enemy or wall is destroyed 
        if (collision.gameObject.tag == "Wall") {
            StopCoroutine("StartAttack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        //lose game once enemies enter castle
        if (collision.gameObject.tag == "CastleFloor") {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("GameOverScene");
        }
    }

    //allows other scripts to set zombie movement
    public void SetDirection(int dir) {
        direction = dir;
        SetMovement();
    }

    public int GetDirection() {
        return direction;
    }

    //When the enemy is enabled, set the direction and movement
    private void OnEnable() {
        SetMovement();
    }

    private void SetMovement() {

        anim = GetComponent<Animator>();    //switch sprite facing directions

        //set speed and sprite direction based on set direction
        switch (direction) {
            case (int)Direction.UP:
                movement = new Vector2(0, speed);    //MOVES UP
                anim.SetInteger("Facing", 2);
                break;
            case (int)Direction.DOWN:
                movement = new Vector2(0, -speed);    //MOVES DOWN
                anim.SetInteger("Facing", 0);
                break;
            case (int)Direction.LEFT:
                movement = new Vector2(-speed, 0);    //MOVES LEFT
                anim.SetInteger("Facing", 1);
                break;
            case (int)Direction.RIGHT:
                movement = new Vector2(speed, 0);    //MOVES RIGHT
                anim.SetInteger("Facing", 3);
                break;

        }
    }

    IEnumerator StartAttack(Collision2D collision) {

        WallController wc = collision.gameObject.GetComponent<WallController>();    //get wall controller

        while (wc.GetHealth() > 0) {
            yield return new WaitForSecondsRealtime(attackSpeed);
            wc.Hit();   //hit wall

        }

        StopCoroutine("StartAttack");

    }
}
