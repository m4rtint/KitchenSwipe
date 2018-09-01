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
        GetComponent<FoodTimer>().ResetFoodTimerIfNeeded();
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
        orderTimerDelegate(this.direction);
        ScoreManager.instance.resetCombo();
    }


    void removeFood(FoodAnimation food)
    {
        food.GetComponent<FoodAnimation>().CompleteFoodAnimationDelegate -= removeFood;
        food.GetComponent<FoodAnimation>().CompleteIngredientPlacementAnimationDelegate -= completedFood;
        orderDelegate(this.direction);
    }

    #endregion

    #region helper
    void completedFood()
    {
        if (!storedFood.IsFoodInPlay())
        {

            storedFood.GetComponent<FoodAnimation>().StartFinishFoodAnimation();
            storedFood = null;

            //SCORE
            int score = ScoreManager.instance.incrementScore();
            GetComponent<FoodScore>().risingScoreAnimation(score);

            //TIME
            TimeManager.instance.incrementGameTime();
        }
    }

    void setDelegate()
    {
        if (storedFood != null)
        {
            storedFood.GetComponent<FoodAnimation>().CompleteFoodAnimationDelegate += removeFood;
            storedFood.GetComponent<FoodAnimation>().CompleteIngredientPlacementAnimationDelegate += completedFood;
        }
    }
    #endregion
}
