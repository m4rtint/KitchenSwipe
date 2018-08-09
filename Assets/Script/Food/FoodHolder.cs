using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FoodTimer))]
public class FoodHolder : MonoBehaviour {

    public delegate void FoodHolderOrderDelegate( Direction dir);
    public FoodHolderOrderDelegate OrderDelegate;

	public delegate void FoodHolderOrderTimerDelegate(Direction dir);
	public FoodHolderOrderTimerDelegate OrderTimerDelegate;

    Food m_StoredFood;
    Direction direction;

    #region Getters/Setter
    public void SetDirection(int dir)
    {
        this.direction = (Direction)dir;
    }

	public void SetStoredFood(Food food) {
		m_StoredFood = food;
        SetDelegate();
        GetComponent<FoodTimer>().ResetFoodTimer();
    }

    void SetDelegate()
    {
        if (m_StoredFood != null)
        {
            m_StoredFood.GetComponent<FoodAnimation>().CompleteFoodAnimationDelegate += RemoveFood;
            m_StoredFood.GetComponent<FoodAnimation>().CompleteIngredientPlacementAnimationDelegate += CompletedFood;
        }
    }

	public Food GetStoredFood(){
		return m_StoredFood;
	}
    #endregion

    #region UserAction
    public void CorrectlySwiped()
    {
        //Decrement level of ingredient
        m_StoredFood.PlaceIngredient();
    }

    void CompletedFood()
    {
        if (m_StoredFood.GetIngredientLevel() == -1)
        {
            m_StoredFood.GetComponent<FoodAnimation>().StartFinishFoodAnimation();
            m_StoredFood = null;

            //SCORE
            ScoreManager.instance.IncrementScore();
            //TIME
            TimeManager.instance.IncrementGameTime();
        }
    }

	public void IncorrectlySwiped(){
		OrderTimerDelegate (this.direction);
	}


    void RemoveFood(FoodAnimation food)
    {
        food.GetComponent<FoodAnimation>().CompleteFoodAnimationDelegate -= RemoveFood;
        food.GetComponent<FoodAnimation>().CompleteIngredientPlacementAnimationDelegate -= CompletedFood;
        OrderDelegate(this.direction);
    }

#endregion
}
