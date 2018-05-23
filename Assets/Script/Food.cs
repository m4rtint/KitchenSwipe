using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
	[SerializeField]
	string m_FoodName;
	[SerializeField]
	Ingredient[] m_Ingredients;


	public Ingredient[] GetIngredients() {
		return m_Ingredients;
	}
}

