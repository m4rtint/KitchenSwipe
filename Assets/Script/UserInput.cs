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

	public delegate void InputDelegate(Direction dir);
	public InputDelegate thisDelegate;

    //TODO - Temperary swipe detection. Fix
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			thisDelegate (Direction.Left);
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			thisDelegate (Direction.Right);
		}
		else if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			thisDelegate (Direction.Up);
		}
		else if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			thisDelegate (Direction.Down);
		}
	}
}

