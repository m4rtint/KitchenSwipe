﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Direction {
	Left,
    Right,
    Up,
    Down
}

public class UserInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public delegate void InputDelegate(Direction dir);
    public InputDelegate swipeDelegate;

    Direction currentDirection;

    [SerializeField]
    float m_DragDistance;

    Direction GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        Direction draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? Direction.Right : Direction.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? Direction.Up : Direction.Down;
        }

#if UNITY_EDITOR
        if (DebugManager.instance.C_Direction)
        {
            Debug.Log(draggedDir);
        }
#endif
        return draggedDir;
    }

    bool InGame()
    {
        return StateManager.instance.InGame();
    }

    public void RunSwipeDelegate()
    {
        this.swipeDelegate(currentDirection);
    }


    #region Events
    //Do this when the user stops dragging this UI Element.
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 displacement = eventData.position - eventData.pressPosition;

        Vector3 dragVectorDirection = displacement.normalized;
        currentDirection = GetDragDirection(dragVectorDirection);

        float distance = displacement.magnitude;
        if (distance > m_DragDistance && InGame())
        {
            GetComponent<CenterIngredientMovement>().StartSnapOffScreenAnimation(currentDirection);
        } 
    }

    public void OnBeginDrag(PointerEventData eventData) {}

    public void OnDrag(PointerEventData eventData){ }
    #endregion

    

}
