using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;    //movement speed
    private Rigidbody2D rb;

    public Transform arrowSpawn;    //spawn point for arrows
    private GameObject[] arrows = new GameObject[2];    //recycling arrow objects
    private bool bCanShoot; //is player allowed to shoot

    public int health = 1;

    private int curDirection;   //the direction the player is currently facing

    Animator anim;

    // Use this for initialization
    void Start () {

        //gets animator, starts sprite facing down
        anim = GetComponent<Animator>();
        anim.SetInteger("Facing", 0);

        rb = GetComponent<Rigidbody2D>();

        //loads arrows
        arrows[0] = Instantiate(Resources.Load("arrow"), arrowSpawn.position, Quaternion.identity) as GameObject;
        arrows[1] = Instantiate(Resources.Load("arrow"), arrowSpawn.position, Quaternion.identity) as GameObject;

        //deactivates arrows
        arrows[0].SetActive(false);
        arrows[1].SetActive(false);

        //gets each arrow's script
        foreach(GameObject arrow in arrows) {
            arrow.GetComponent<ArrowController>();
        }

        bCanShoot = true;   //player is able to shoot

        curDirection = (int)Direction.DOWN; //player starts facing down
    }

    // Update is called once per frame
    void Update() {

        //get the button inputs
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveH, moveV);

        //DIRECTION CHANGES

        if(moveH > 0) {
            curDirection = (int)Direction.RIGHT;
            anim.SetInteger("Facing", 3);
        } else if(moveH < 0) {
            curDirection = (int)Direction.LEFT;
            anim.SetInteger("Facing", 1);
        }

        if (moveV > 0) {
            curDirection = (int)Direction.UP;
            anim.SetInteger("Facing", 2);
        } else if (moveV < 0) {
            curDirection = (int)Direction.DOWN;
            anim.SetInteger("Facing", 0);
        }

        rb.velocity = movement * speed; //move player

        //Shoots arrows with SPACE
        if (Input.GetKeyDown("space")) {

            foreach (GameObject arrow in arrows) {

                if (!arrow.activeSelf && bCanShoot) { //if bullet is not active

                    arrow.transform.position = arrowSpawn.position;
                    arrow.GetComponent<ArrowController>().SetDirection(curDirection);
                    arrow.SetActive(true);
                    bCanShoot = false;
                    StartCoroutine("BulletCooldown");
                    return;
                }

            }
        }
    }

    //if touched by an enemy player will lose game
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Debug.Log("U DEAD");
            SceneManager.LoadScene("GameOverScene");
        }
    }

    //cooldown to prevent spamming arrows
    IEnumerator BulletCooldown() {
        yield return new WaitForSecondsRealtime(0.5f);
        bCanShoot = true;
        StopCoroutine("BulletCooldown");
    }
}
