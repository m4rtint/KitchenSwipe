using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour {

	public void PauseOrStartGame()
    {
        if (StateManager.instance.InGame())
        {
            StateManager.instance.PauseGame();
        } else
        {
            StateManager.instance.StartGame();
        }
    }
}
