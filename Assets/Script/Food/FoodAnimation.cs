﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Food))]
public class FoodAnimation : MonoBehaviour {


    //Delegate
    public delegate void FoodAnimationDelegate(FoodAnimation food);
    public FoodAnimationDelegate CompleteFoodAnimationDelegate;

    public delegate void IngredientPlacedDelegate();
    public IngredientPlacedDelegate CompleteIngredientPlacementAnimationDelegate;

    //Fade out
    readonly float m_TimeTakenToFade = 1.0f;
    readonly Vector3 m_RiseUp = new Vector3(0, 100, 0);

    Ingredient[] m_Ingredients;
    Food m_Food;

    Vector3 m_EndPosition;
    RectTransform m_RectTrans;

    #region mono
    void Awake()
    {
        m_RectTrans = GetComponent<RectTransform>();
        m_Food = GetComponent<Food>();
        m_Ingredients = m_Food.GetIngredients();
        SetupDelegate();
    }

    void Start()
    {
        m_EndPosition = m_RectTrans.transform.localPosition + m_RiseUp;
    }

    void SetupDelegate()
    {
        foreach(Ingredient ingredient in m_Ingredients)
        {
            ingredient.PlacementIngredientDelegate += PlaceIngredientAnimComplete;
        }
    }

    void PlaceIngredientAnimComplete()
    {
        CompleteIngredientPlacementAnimationDelegate();
        m_Food.activeTopIngredientIfNeeded();
    }

    void CompletedMovingUp()
    {
        CompleteFoodAnimationDelegate(this);
        Destroy(gameObject);

    }
    #endregion

    #region Public
    public void StartFinishFoodAnimation()
    {
        Hashtable ht = new Hashtable();
        ht.Add("y", 50);
        ht.Add("time", m_TimeTakenToFade);
        ht.Add("oncomplete", "CompletedMovingUp");
        iTween.MoveAdd(gameObject, ht);
        SetAlpha();
    }
    #endregion

    #region Colour
    void SetAlpha()
    {
        foreach (Ingredient ingredient in m_Ingredients)
        {
            ingredient.SetCrossFadeAlpha(m_TimeTakenToFade/2);
        }
    }
    #endregion
}
