using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

    public static TransitionManager instance = null;

    private void Awake()
    {
        instance = this;
    }


    public void StartInfiniteGameScene() {
		SceneManager.LoadScene ("InfiniteGameMode");
        StateManager.instance.StartGame ();
	}

    public void StartMainMenuScene()
    {
        SceneManager.LoadScene(0);
        StateManager.instance.SetToMenu();
    }
}
