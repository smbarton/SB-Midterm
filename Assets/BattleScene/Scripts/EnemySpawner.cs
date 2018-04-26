using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    //"enemySpawnPoints"
    private GameObject[] spawnPoints;
    private GameObject[] zombies; //all the plain zombies
    private GameObject[] skeletons; //all the skeletons
    public int normalWaitTime = 2;  //spawn time between zombies
    public int specialWaitTime = 10;    //spawn time between skeletons

    //NUMBER OF ENEMIES to be spawned
    public int zombieCount = 8;
    public int skeletonCount = 2;

    private IEnumerator coroutine;

    // Use this for initialization
    void Start() {

        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");  //get all spawn points

        //Debug.Log("SpawnPoints Found: " + spawnPoints.Length);
        
        //initialize arrays
        zombies = new GameObject[zombieCount];
        skeletons = new GameObject[skeletonCount];

        //populate enemy arrays
        for (int i = 0; i < zombies.Length; i++) {
            GameObject enemy = Instantiate(Resources.Load("zombie"), this.gameObject.transform.position, Quaternion.identity) as GameObject;
            enemy.SetActive(false);
            zombies[i] = enemy;
        }

        for (int i = 0; i < skeletons.Length; i++) {
            GameObject enemy = Instantiate(Resources.Load("skeleton"), this.gameObject.transform.position, Quaternion.identity) as GameObject;
            enemy.SetActive(false);
            skeletons[i] = enemy;
        }

        StartCoroutine("WaitAndSpawn"); //start spawing enemies

    }

    // Update is called once per frame
    void Update() {

    }

    public void SpawnSkeletons() {
        StartCoroutine("SpawnSpecial");
    }

    //Spawns and recycles enemies
    //finds inactive enmy to spawn
    IEnumerator WaitAndSpawn() {
        while (true) {
            yield return new WaitForSecondsRealtime(normalWaitTime);
            for (int i = 0; i < zombies.Length; i++) {
                bool isActive = zombies[i].gameObject.activeSelf;
                if (isActive == false) {
                    int spawn = Random.Range(0, spawnPoints.Length);
                    Debug.Log(i + " at " + spawn + ", pos:" +  spawnPoints[spawn].gameObject.transform.position);

                    zombies[i].transform.position = spawnPoints[spawn].gameObject.transform.position;
                    zombies[i].GetComponent<ZombieController>().SetDirection(spawnPoints[spawn].GetComponentInParent<WallController>().GetEnemyDirection());
                    zombies[i].SetActive(true);
                    break;
                }
            }
        }
    }

    IEnumerator SpawnSpecial() {
        while (true) {
            yield return new WaitForSecondsRealtime(specialWaitTime);
            for (int i = 0; i < skeletons.Length; i++) {
                bool isActive = skeletons[i].gameObject.activeSelf;
                if (isActive == false) {
                    int spawn = Random.Range(0, spawnPoints.Length);
                    Debug.Log(i + " at " + spawn + ", pos:" + spawnPoints[spawn].gameObject.transform.position);

                    skeletons[i].transform.position = spawnPoints[spawn].gameObject.transform.position;
                    skeletons[i].GetComponent<SkeletonController>().SetDirection(spawnPoints[spawn].GetComponentInParent<WallController>().GetEnemyDirection());
                    skeletons[i].SetActive(true);
                    //break;
                }
            }
        }
    }

}
