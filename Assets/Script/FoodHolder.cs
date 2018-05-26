using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodHolder : MonoBehaviour {

    public delegate void FoodHolderDelegate();
    public FoodHolderDelegate thisDelegate;

    Food m_StoredFood;

    #region Getters/Setter

	public void SetStoredFood(Food food) {
		m_StoredFood = food;
	}

	public Food GetStoredFood(){
		return m_StoredFood;
	}
    #endregion

    #region View
    public void UpdateListOfIngredientsView()
    {
        string ingredients = "";
        for (int i = 0; i <= m_StoredFood.GetIngredientLevel(); i++)
        {
            ingredients += m_StoredFood.GetIngredients()[i].Get_IngredientName() + "\n";
        }
        GetComponent<Text>().text = ingredients;
    }
    #endregion

    #region UserAction
    public void CorrectlySwiped()
    {
        //Decrement level of ingredient
        m_StoredFood.PlacedIngredient();
        if (m_StoredFood.GetIngredientLevel() == -1)
        {
            Destroy(m_StoredFood.gameObject);
            thisDelegate();
        } 
        UpdateListOfIngredientsView();
        
    }
    #endregion
}
