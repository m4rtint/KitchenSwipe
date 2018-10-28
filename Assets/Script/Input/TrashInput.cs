﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashInput : MonoBehaviour, IPointerClickHandler
{
    public delegate void TrashInputDelegate();
    public TrashInputDelegate animateDoubleTapDelegate;
    public TrashInputDelegate doubleTapDelegate;

    [SerializeField]
    UserInput userInput;

    readonly int doubleTap = 2;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == doubleTap && userInput.CanSwipe())
        {
            animateDoubleTapDelegate();
            userInput.disableSwipe();
        }
    }

    public void runDoubleTapDelegate()
    {
        doubleTapDelegate();
        userInput.enableSwipe();
    }


}
