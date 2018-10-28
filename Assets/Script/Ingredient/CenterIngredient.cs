﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(IngredientGenerator))]
public class CenterIngredient : MonoBehaviour
{
    [Header("Ingredient Name")]
    [SerializeField]
    GameObject ingredientName;

    [Header("Dependencies")]
    [SerializeField]
    IngredientGenerator ingredientGenerator;
    [SerializeField]
    UserInput userInput;
    [SerializeField]
    TrashInput trashInput;
       
    Vector3 startPosition;
    Vector3 trashPosition;
    Ingredient centerIngredient;
    Direction swipedDirection;
    AnimationManager animation;

    #region Mono
    // Use this for initialization
    void Awake()
    {
        animation = AnimationManager.instance;
        startPosition = transform.position;
        trashPosition = trashInput.transform.localPosition;
        setupDelegate();
    }

    void setupDelegate()
    {
        userInput.snapOffDelegate += startCenterAnimationIfNeeded;
        trashInput.animateDoubleTapDelegate += moveToTrashIfNeeded;
    }

    #endregion

    #region GetterSetter
    Vector3 GetIngredientAtSwipedDirectionPosition()
    {
        FoodHolder holder = ingredientGenerator.FoodHolders()[(int)swipedDirection];
        Food food = holder.StoredFood();
        if (food != null)
        {
            if (food.isFoodInPlay()){
                Ingredient ingredient = holder.StoredFood().GetNeededIngredient();
                return ingredient.GetComponent<RectTransform>().position - startPosition;
            }
        } 

        return holder.GetComponent<RectTransform>().position - startPosition;

    }

    public void Center(Ingredient ingredient)
    {
        centerIngredient = ingredient;

        if (centerIngredient != null) {
            GetComponent<Image>().sprite = centerIngredient.CenterSpriteImage();
            GetComponent<Image>().SetNativeSize();
            setCenterName(ingredient.IngredientName());
        } else {
            setColorClear();
            setCenterName();
        }
    }

    void setCenterName(string text = "")
    {
        ingredientName.GetComponent<TextMeshProUGUI>().text = text;
    }
    #endregion

    #region Movement
    void startCenterAnimationIfNeeded(Direction dir)
    {
        this.swipedDirection = dir;
        if (shouldRunSwipe()) {
            setCenterName();
            Vector3 endPosition = GetIngredientAtSwipedDirectionPosition();
            if (centerIngredient.GetType() == typeof(Sauce))
            {
                RotateCenterIngredient(dir);
            }
            else
            {
                moveCenterTo(endPosition, "moveCenterDelegate");
            }
        } else
        {
            userInput.enableSwipe();
        }
    }

    void RotateCenterIngredient(Direction dir)
    {
        Hashtable ht = new Hashtable();
        ht.Add("z", angleForRotation(dir));
        ht.Add("easeType", "easeInOutBack");
        ht.Add("time", animation.RotationTime());
        ht.Add("oncomplete", "FadeOutCenter");
        iTween.RotateBy(gameObject, ht);
    }

    float angleForRotation(Direction dir)
    {
        float angle = 40;
        switch (dir)
        {
            case Direction.Down:
                angle = -140; break;
            case Direction.Up:
                angle = 40; break;
            case Direction.Left:
                angle = 130; break;
            case Direction.Right:
                angle = -50;break;
        }
        return angle / 360;
    }


    void moveCenterDelegate()
    {
        resetCenterAnimation();
        userInput.runSwipeDelegate();
    }

    void resetCenterAnimation()
    {
        GetComponent<Image>().color = Color.white;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
    #endregion

    #region Trash
    void moveToTrashIfNeeded()
    {
        if (centerIngredient != null)
        {
            moveToTrash();
        }
    }

    void moveToTrash()
    {
        //Size
        iTween.ScaleTo(gameObject, Vector3.one * 0.5f, animation.CenterMoveTime());
        //Movement
        moveCenterTo(trashPosition, "moveToTrashDelegate");
    }

    void moveToTrashDelegate()
    {
        resetCenterAnimation();
        trashInput.runDoubleTapDelegate();
    }

    #endregion

    #region tools
    bool shouldRunSwipe()
    {
        return (int)swipedDirection < ingredientGenerator.foodHolderLength();
    }

    void setColorClear()
    {
        GetComponent<Image>().sprite = null;
        GetComponent<Image>().color = Color.clear;
    }

    void moveCenterTo(Vector3 position, string onComplete)
    {
        Hashtable ht = new Hashtable();
        ht.Add("x", position.x);
        ht.Add("y", position.y);
        ht.Add("easeType", "easeOutCubic");
        ht.Add("time", animation.CenterMoveTime());
        ht.Add("oncomplete", onComplete);
        iTween.MoveBy(gameObject, ht);
    }
    #endregion

}
