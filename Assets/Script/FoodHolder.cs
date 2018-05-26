using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodHolder : MonoBehaviour {

	Food m_StoredFood;

    #region Getters/Setter

	public void SetStoredFood(Food food) {
		m_StoredFood = food;
	}

	public Food GetStoredFood(){
		return m_StoredFood;
	}
    #endregion

    public void UpdateListOfIngredientsView()
    {
        string ingredients = "";
        for (int i = 0; i <= m_StoredFood.GetIngredientLevel(); i++)
        {
            ingredients += m_StoredFood.GetIngredients()[i].Get_IngredientName() + "\n";
        }
        GetComponent<Text>().text = ingredients;
    }
}
