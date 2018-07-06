using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

	public void StartInfiniteGameScene() {
		SceneManager.LoadScene (1);
		StateManager.instance.StartGame ();
	}
}
