using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	[SerializeField]
	string m_FoodName;
	[SerializeField]
	Ingredient[] m_Ingredients;
	[SerializeField]
	float m_SecondsToComplete;

	int m_Level;

	#region Getter/Setter
	public string GetFoodName() {
		return m_FoodName;
	}

	public Ingredient[] GetIngredients() {
		return m_Ingredients;
	}

	public Ingredient GetNeededIngredient() {
		return m_Ingredients [m_Level];
	}

	public int GetIngredientLevel() {
		return m_Level;
	}

	public void SetupIngredientLevel() {
		m_Level = m_Ingredients.Length-1;
	}

	public void DecrementSecondsBy(float seconds) {
		m_SecondsToComplete -= seconds;
	}
    
	public float GetSecondsToComplete() {
		return m_SecondsToComplete;
	}
	#endregion


	#region UserAction
	public void PlacedIngredient() {
		m_Level--;
	}
	#endregion
}

