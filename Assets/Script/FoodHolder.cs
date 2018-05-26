using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodHolder : MonoBehaviour {

	public Direction dir;
	Food m_StoredFood;

	bool m_IsEmpty = true;

	public bool IsEmpty(){
		return m_IsEmpty;
	}

	public void SetStoredFood(Food food) {
		m_StoredFood = food;
	}

	public Food GetStoredFood(){
		return m_StoredFood;
	}

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
