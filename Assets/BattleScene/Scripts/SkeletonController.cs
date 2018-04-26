using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SkeletonController : MonoBehaviour {

    public float speed = 1.5f;
    public int health = 1;

    private int direction;

    private Rigidbody2D rb;

    public int attackSpeed = 2;

    private Vector2 movement;

    private IEnumerator coroutine;

    Animator anim;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        //direction = (int)Direction.UP;
        SetMovement();
    }

    private void FixedUpdate() {

        rb.velocity = movement;

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            WallController wc = collision.gameObject.GetComponent<WallController>();
            wc.Hit();
            coroutine = StartAttack(collision);
            StartCoroutine(coroutine);
        } else if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            StopCoroutine("StartAttack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "CastleFloor") {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public void SetDirection(int dir) {
        direction = dir;
        SetMovement();
    }

    public int GetDirection() {
        return direction;
    }

    private void OnEnable() {
        SetMovement();
    }

    private void SetMovement() {

        anim = GetComponent<Animator>();

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

        WallController wc = collision.gameObject.GetComponent<WallController>();

        while (wc.GetHealth() > 0) {
            //Debug.Log("Starting Attack...");
            yield return new WaitForSecondsRealtime(attackSpeed);
            wc.Hit();
            //Debug.Log("Wall Hit!");
            //StopCoroutine(coroutine);
        }

        //Debug.Log("Stopping Attack");

        StopCoroutine("StartAttack");

    }
}
