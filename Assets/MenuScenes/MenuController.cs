using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //LOAD SCENES

    public void LoadNormalLevel() {
        SceneManager.LoadScene("BattleScene");
    }

    public void LoadHardLevel() {
        SceneManager.LoadScene("HardLevel");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    //QUIT GAME

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
