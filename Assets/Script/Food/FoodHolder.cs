﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodTimer))]
[RequireComponent(typeof(FoodScore))]
public class FoodHolder : MonoBehaviour
{

    public delegate void FoodHolderOrderDelegate(Direction dir);
    public FoodHolderOrderDelegate orderDelegate;
    public FoodHolderOrderDelegate orderTimerDelegate;

    Food storedFood;
    Direction direction;
    Vector3 trashPosition;

    #region mono
    private void Start()
    {
        trashPosition = AnimationManager.instance.TrashPosition() - transform.localPosition;
    }
    #endregion

    #region Getters/Setter
    public FoodTimer FoodTimer()
    {
        return GetComponent<FoodTimer>();
    }

    public void Direction(int dir)
    {
        this.direction = (Direction)dir;
    }

    public void StoredFood(Food food)
    {
        storedFood = food;
        setDelegate();
        FoodTimer().TimerObject(true);
        FoodTimer().resetFoodTimerIfNeeded();
    }

    public Food StoredFood()
    {
        return storedFood;
    }

    bool isStoredFoodNull()
    {
        return storedFood == null;
    }

    void removeStoredFood()
    {
        storedFood = null;
    }
    #endregion

    #region UserAction
    public void correctlySwiped()
    {
        //Decrement level of ingredient
        storedFood.PlaceIngredient();
        if (!storedFood.isFoodInPlay())
        {
            FoodTimer().pause();
        } 
    }

    public void incorrectlySwiped()
    {
        if (!isStoredFoodNull())
        {
            storedFood.Animation().shakeFood();
            orderTimerDelegate(this.direction);
        }
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.resetCombo();
        }
    }


    void removeFood(FoodAnimation food)
    {
        food.completeFoodAnimationDelegate -= removeFood;
        food.completeIngredientPlacementAnimationDelegate -= completedFood;
        orderDelegate(this.direction);
    }

    public void moveFoodToTrashIfneeded()
    {
        if (!isStoredFoodNull())
        {
            FoodAnimation foodAnim = StoredFood().Animation();
            foodAnim.moveToTrash(trashPosition);
            removeStoredFood();
            disableFoodTimer();
        }
    }

    #endregion

    #region helper

    void disableFoodTimer()
    {
        FoodTimer().pause();
        FoodTimer().TimerObject(false);
    }

    void completedFood()
    {
        if (!isStoredFoodNull() && !storedFood.isFoodInPlay())
        {
            storedFood.Animation().StartFinishFoodAnimation();
            OverlayParticles.ShowParticles(7, storedFood.transform.position);
            removeStoredFood();

            //SCORE
            if (ScoreManager.instance != null)
            {
                int score = ScoreManager.instance.incrementScore();
                GetComponent<FoodScore>().risingScoreAnimation(score);
            }

            //TIME
            if (TimeManager.instance != null) {
                TimeManager.instance.incrementGameTime();
            }
        }
    }

    void setDelegate()
    {
        if (storedFood != null)
        {
            storedFood.Animation().completeFoodAnimationDelegate += removeFood;
            storedFood.Animation().completeIngredientPlacementAnimationDelegate += completedFood;
        }
    }
    #endregion
}
