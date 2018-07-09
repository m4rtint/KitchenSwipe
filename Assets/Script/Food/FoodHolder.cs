using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodHolder : MonoBehaviour {

    public delegate void FoodHolderOrderDelegate( Food food);
    public FoodHolderOrderDelegate OrderDelegate;

	public delegate void FoodHolderOrderTimerDelegate( Food food);
	public FoodHolderOrderTimerDelegate OrderTimerDelegate;

    Food m_StoredFood;

    #region Getters/Setter

	public void SetStoredFood(Food food) {
		m_StoredFood = food;
	}

	public Food GetStoredFood(){
		return m_StoredFood;
	}
    #endregion

    #region UserAction
    public void CorrectlySwiped()
    {
        //Decrement level of ingredient
        m_StoredFood.PlacedIngredient();
        if (m_StoredFood.GetIngredientLevel() == -1)
		{
			Food tempStoredFood = m_StoredFood;
			RemoveFood();
			OrderDelegate(tempStoredFood);
            //SCORE
            ScoreManager.instance.IncrementScore();
            //TIME
            TimeManager.instance.IncrementGameTime();
        } 

    }

	public void IncorrectlySwiped(){
		OrderTimerDelegate (m_StoredFood);
	}


    void RemoveFood()
    {
        Destroy(m_StoredFood.gameObject);
        m_StoredFood = null;
    }

#endregion
}
