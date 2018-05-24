using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
	[SerializeField]
	string m_FoodName;
	[SerializeField]
	Ingredient[] m_Ingredients;

	int m_Level;

	#region Getter/Setter
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
	#endregion


	#region UserAction
	public void PlacedIngredient() {
		m_Level--;
	}
	#endregion
}

