﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBun : Ingredient {

    //BackBun Properties
    [SerializeField]
    GameObject m_BackBun;
    Vector3 m_BackbunEndPosition;
    RectTransform m_BackBunTrans;
    

    private void OnEnable()
    {
        m_BackBun.SetActive(true);
        m_BackBunTrans = m_BackBun.GetComponent<RectTransform>();
    }

    public override void SetAlpha(float percent)
    {
        base.SetAlpha(percent);
        m_BackBun.GetComponent<Image>().color = new Color(1,1,1,percent);
    }

    public override void StartAnimation()
    {
        base.StartAnimation();
        Hashtable ht = new Hashtable();
        ht.Add("y", -base.m_PlaceDownDistance);
        ht.Add("easeType", "easeInOutExpo");
        ht.Add("time", m_TimeTakenToPlace);
        ht.Add("oncomplete", "WaitForPlaceDelegate");
        iTween.MoveAdd(m_BackBun, ht);
    }

    protected override void WaitForPlaceDelegate()
    {
        base.WaitForPlaceDelegate();
        PlacementIngredientDelegate();
    }


    public override void SetCrossFadeAlpha(float duration)
    {
        base.SetCrossFadeAlpha(duration);
        Hashtable ht = new Hashtable();
        ht.Add("a", 0);
        ht.Add("time", duration);
        iTween.ColorTo(gameObject, ht);
    }
}
