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


    public void startInfiniteGameScene() {
		SceneManager.LoadScene ("InfiniteGameMode");
        StateManager.instance.startGame ();
	}

    public void startMainMenuScene()
    {
        SceneManager.LoadScene(0);
        StateManager.instance.setToMenu();
    }
}
