using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    public int health = 3;  //how many hits the wall can take
    public int spawnDistance = 2;   //how far enemies will spawn

    Animator anim;

    //DIRECTIONS
    public Direction spawnDirection;    //direction from wall that spawn point will be placed
    private int enemyDirection;         //direction that the enemy will move when spawned

    public GameObject spawnPoint;   //attached spawn point

    private bool doesNotSpawn = false;

    // Use this for initialization
    void Start () {
        if (spawnPoint == null) {
            doesNotSpawn = true;
        } else {
            MoveSpawnPoint();
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        //can never have more than 3 health or less than 0
        if(health > 3) {
            health = 3;
        } else if(health < 0) {
            health = 0;
        }

        //sets the animator
        anim = GetComponent<Animator>();
        anim.SetInteger("CurrentState", health);

        //disable wall if health reaches zero
        if (health == 0) {
            GetComponent<Collider2D>().enabled = false;
        }

    }

    //moves the spawn point based on set direction
    private void MoveSpawnPoint() {
        switch (spawnDirection) {
            case Direction.UP:
                spawnPoint.transform.localPosition = new Vector2(0, spawnDistance);
                enemyDirection = (int)Direction.DOWN;
                break;
            case Direction.DOWN:
                spawnPoint.transform.localPosition = new Vector2(0, -spawnDistance);
                enemyDirection = (int)Direction.UP;
                break;
            case Direction.LEFT:
                spawnPoint.transform.localPosition = new Vector2(-spawnDistance, 0);
                enemyDirection = (int)Direction.RIGHT;
                break;
            case Direction.RIGHT:
                spawnPoint.transform.localPosition = new Vector2(spawnDistance, 0);
                enemyDirection = (int)Direction.LEFT;
                break;

        }
    }

    //when wall is hit, lower health by one
    public void Hit() {
        health--;
    }

    //SETTERS and GETTERS

    public void SetHealth(int hlth) {
        health = hlth;
    }

    public int GetHealth() {
        return health;
    }

    public int GetEnemyDirection() {
        return enemyDirection;
    }
}
