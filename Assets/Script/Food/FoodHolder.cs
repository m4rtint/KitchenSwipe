using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodHolder : MonoBehaviour {

	#if UNITY_EDITOR
	public bool DEBUG_FOOD_NAME;
	#endif

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

	#if UNITY_EDITOR
    public void UpdateListOfIngredientsView()
    {
		if (m_StoredFood == null) { return; }
        string ingredients = "";
        for (int i = 0; i <= m_StoredFood.GetIngredientLevel(); i++)
        {
            ingredients += m_StoredFood.GetIngredients()[i].Get_IngredientName() + "\n";
        }
		SetIngredientsView(ingredients);
    }

	void SetIngredientsView(string ingredients) {
		if (!DEBUG_FOOD_NAME) {
			ingredients = "";
		} 
		GetComponent<Text> ().text = ingredients;
	}
	#endif

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
		#if UNITY_EDITOR
        UpdateListOfIngredientsView();
		#endif
        
    }

	public void IncorrectlySwiped(){
		OrderTimerDelegate (m_StoredFood);
	}


    void RemoveFood()
    {
        Destroy(m_StoredFood.gameObject);

        m_StoredFood = null;
#if UNITY_EDITOR
        SetIngredientsView("Empty");
#endif
    }

#endregion
}
