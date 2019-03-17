using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Menu,
    PrepareGame,
    Game,
    Pause,
    GameOver
}
public class StateManager : MonoBehaviour
{
    public static StateManager instance = null;
    public delegate void OnStateChangeDelegate();
    public static event OnStateChangeDelegate stateChangedDelegate;

    [SerializeField]
    GameState _CurrentState;
    public GameState currentState
    {
        get
        {
            return _CurrentState;
        }

        private set
        {
            _CurrentState = value;
            stateChangedDelegate();
        }
    }

    GameState savedRevertState;

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

    #region GameStates
    public void prepareGame()
    {
        currentState = GameState.PrepareGame;
    }

    public void startGame()
    {
        currentState = GameState.Game;
    }

    public void pauseGame()
    {
        savedRevertState = currentState;
        currentState = GameState.Pause;
    }

    public void unPausedGame()
    {
        currentState = savedRevertState;
    }

    public void setToMenu()
    {
        currentState = GameState.Menu;
    }

    public void gameOver()
    {
        currentState = GameState.GameOver;
    }

    public bool isInGame()
    {
        return currentState == GameState.Game;
    }


    public bool isPaused()
    {
        return currentState == GameState.Pause;
    }

    public bool isPreparingGame()
    {
        return currentState == GameState.PrepareGame;
    }

    #endregion
}
