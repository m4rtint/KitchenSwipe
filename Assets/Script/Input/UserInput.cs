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
    public InputDelegate snapOffDelegate;
    
    Direction currentDirection;

    [SerializeField]
    float dragDistance;
    bool canSwipe = true;
    Direction dragDirection(Vector3 dragVector)
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
        return draggedDir;
    }

    bool CanSwipe()
    {
        return StateManager.instance.isInGame() && canSwipe;
    }

    public void RunSwipeDelegate()
    {
        this.swipeDelegate(currentDirection);
        canSwipe = true;
    }


    #region Events
    //Do this when the user stops dragging this UI Element.
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 displacement = eventData.position - eventData.pressPosition;
        Vector3 dragVectorDirection = displacement.normalized;

        float distance = displacement.magnitude;
        if (distance > dragDistance && canSwipe)
        {
            canSwipe = false;
            currentDirection = dragDirection(dragVectorDirection);
            this.snapOffDelegate(currentDirection);
        } 
    }

    public void OnBeginDrag(PointerEventData eventData) {}

    public void OnDrag(PointerEventData eventData){ }
    #endregion

    

}

