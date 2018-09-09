using System.Collections;
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
    readonly Vector3 m_RiseUp = new Vector3(0, 100, 0);

    Ingredient[] m_Ingredients;
    Food m_Food;
    AnimationManager animation;
    Vector3 m_EndPosition;
    RectTransform m_RectTrans;

    #region mono
    void Awake()
    {
        animation = AnimationManager.instance;
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
            ingredient.placementIngredientDelegate += PlaceIngredientAnimComplete;
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
        ascendUpwards();
        SetAlpha();
    }

    public void shakeFood()
    {
        Hashtable ht = new Hashtable();
        ht.Add("x", animation.FoodShakeAmount());
        ht.Add("time", animation.FoodShakeTime());
        iTween.ShakePosition(gameObject, ht);
    }

    #endregion

    #region Animation
    void ascendUpwards()
    {
        Hashtable ht = new Hashtable();
        ht.Add("y", animation.AscensionAmount());
        ht.Add("time", animation.AscensionTime());
        ht.Add("easeType", "easeOutCubic");
        ht.Add("oncomplete", "CompletedMovingUp");
        iTween.MoveAdd(gameObject, ht);
    }

    void SetAlpha()
    {
        iTween.FadeTo(gameObject, 0, animation.AscensionFade());
    }
    #endregion
}
