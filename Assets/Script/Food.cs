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

	#region Mono
	void Awake(){
		SetupIngredientLevel ();
	}
	#endregion

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

	void SetupIngredientLevel() {
		m_Level = m_Ingredients.Length-1;
	}
    
	//For Order timing on top
	public float GetSecondsToComplete() {
		return m_SecondsToComplete+Random.Range(3,10);
	}
	#endregion


	#region UserAction
	public void PlacedIngredient() {
		m_Level--;
	}
	#endregion
}

