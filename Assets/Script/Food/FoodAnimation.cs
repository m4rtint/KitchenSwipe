using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Food))]
public class FoodAnimation : MonoBehaviour {

    //Delegate
    public delegate void FoodAnimationDelegate(FoodAnimation food);
    public FoodAnimationDelegate completeFoodAnimationDelegate;

    public delegate void IngredientPlacedDelegate();
    public IngredientPlacedDelegate completeIngredientPlacementAnimationDelegate;

    //Fade out
    readonly Vector3 riseUp = new Vector3(0, 100, 0);

    Ingredient[] ingredients;
    Food food;
    AnimationManager animation;
    Vector3 endPosition;

    #region mono
    void Awake()
    {
        animation = AnimationManager.instance;
        food = GetComponent<Food>();
        ingredients = food.Ingredients();
        setupDelegate();
    }

    void Start()
    {
        endPosition = transform.localPosition + riseUp;
    }

    void setupDelegate()
    {
        foreach(Ingredient ingredient in ingredients)
        {
            ingredient.placementIngredientDelegate += PlaceIngredientAnimComplete;
        }
    }

    void PlaceIngredientAnimComplete()
    {
        completeIngredientPlacementAnimationDelegate();
        food.activeTopIngredientIfNeeded();
    }

    void removeFood()
    {
        completeFoodAnimationDelegate(this);
        Destroy(gameObject);

    }
    #endregion

    #region Public
    public void StartFinishFoodAnimation()
    {
        ascendUpwards();
        setAlpha();
    }

    public void shakeFood()
    {
        Hashtable ht = new Hashtable();
        ht.Add("x", animation.FoodShakeAmount());
        ht.Add("time", animation.FoodShakeTime());
        iTween.ShakePosition(gameObject, ht);
    }

    public void moveToTrash(Vector3 position)
    {
        //Scale
        iTween.ScaleTo(gameObject, Vector3.one * 0.5f, animation.CenterMoveTime());

        Hashtable ht = new Hashtable();
       // ht.Add("x", position.x);
        //ht.Add("y", position.y);
        ht.Add("position", position);
        ht.Add("islocal", true);
        ht.Add("easeType", "easeOutCubic");
        ht.Add("time", animation.MoveToTrashTime());
        ht.Add("oncomplete", "removeFood");
        iTween.MoveTo(gameObject, ht);
    }

    #endregion

    #region Animation
    void ascendUpwards()
    {
        Hashtable ht = new Hashtable();
        ht.Add("y", animation.AscensionAmount());
        ht.Add("time", animation.AscensionTime());
        ht.Add("easeType", "easeOutCubic");
        ht.Add("oncomplete", "removeFood");
        iTween.MoveAdd(gameObject, ht);
    }

    public void quicklyChangeFoodColor(Color c)
    {
        iTween.ColorFrom(gameObject, c, animation.FoodShakeTime());
    }

    void setAlpha()
    {
        iTween.FadeTo(gameObject, 0, animation.AscensionFade());
    }
    #endregion
}
