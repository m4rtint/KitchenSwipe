using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Menu,
    Game,
    Pause,
    GameOver
}
public class StateManager : MonoBehaviour {

    public static StateManager instance = null;

	[SerializeField]
    GameState m_CurrentState;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
	}
		
	public void PauseOrStartGame(){
		if (InGame ()) {
			m_CurrentState = GameState.Pause;
		} else {
			m_CurrentState = GameState.Game;
		}
	}

	public void EndGame() {
		m_CurrentState = GameState.GameOver;
	}

	public bool InGame(){
		return m_CurrentState == GameState.Game;
	}

	public bool Paused(){
		return m_CurrentState == GameState.Pause;
	}
}
