using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public float targetTime = 60f;  //time to reach for victory
    public float specialTime = 40f; //time that special enemies will spawn
    public Text timer;  //displaying current time

    public EnemySpawner enemySpawner;   

    private bool isSpecialCalled;   //flag to prevent calling same function infinitely

	// Use this for initialization
	void Start () {
        enemySpawner.GetComponent<EnemySpawner>();
        isSpecialCalled = false;
	}

    // Update is called once per frame
    void Update () {
        //SIMPLE TIMER 
        targetTime -= Time.deltaTime;

        timer.text = Math.Truncate(targetTime).ToString();

        //once it reaches 0 - player wins
        //once it reaches specialTime, special enemies will spawn
        if (targetTime <= 0.0f) {
            TimerEnded();
        } else if(targetTime < specialTime && isSpecialCalled == false) {
            enemySpawner.SpawnSkeletons();
            isSpecialCalled = true;
        }
    }

    //Declare victory when timer ends - load victory scene
    private void TimerEnded() {
        Debug.Log("You Win!");
        SceneManager.LoadScene("VictoryScene");
    }
}
