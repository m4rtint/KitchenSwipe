using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction {
	Left,
    Right,
    Up,
    Down
}

public class UserInput:MonoBehaviour{

    //TODO - Temperary swipe detection. Fix
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			Debug.Log("Left");
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			Debug.Log("Right");
		}
		else if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			Debug.Log("Up");
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			Debug.Log("Down");
		}
	}
}

