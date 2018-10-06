using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodTimer))]
[RequireComponent(typeof(FoodScore))]
public class FoodHolder : MonoBehaviour
{

    public delegate void FoodHolderOrderDelegate(Direction dir);
    public FoodHolderOrderDelegate orderDelegate;

    public delegate void FoodHolderOrderTimerDelegate(Direction dir);
    public FoodHolderOrderTimerDelegate orderTimerDelegate;

    Food storedFood;
    Direction direction;

    #region Getters/Setter
    public void Direction(int dir)
    {
        this.direction = (Direction)dir;
    }

    public void StoredFood(Food food)
    {
        storedFood = food;
        setDelegate();
        GetComponent<FoodTimer>().resetFoodTimerIfNeeded();
    }

    public Food StoredFood()
    {
        return storedFood;
    }
    #endregion

    #region UserAction
    public void correctlySwiped()
    {
        //Decrement level of ingredient
        storedFood.PlaceIngredient();
    }

    public void incorrectlySwiped()
    {
        if (storedFood != null)
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
        food.CompleteFoodAnimationDelegate -= removeFood;
        food.CompleteIngredientPlacementAnimationDelegate -= completedFood;
        orderDelegate(this.direction);
    }

    #endregion

    #region helper
    void completedFood()
    {
        if (!storedFood.isFoodInPlay())
        {

            storedFood.Animation().StartFinishFoodAnimation();
            OverlayParticles.ShowParticles(7, storedFood.transform.position);
            storedFood = null;

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
            storedFood.Animation().CompleteFoodAnimationDelegate += removeFood;
            storedFood.Animation().CompleteIngredientPlacementAnimationDelegate += completedFood;
        }
    }
    #endregion
}
