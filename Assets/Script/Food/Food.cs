using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

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
        m_SecondsToComplete += Random.Range(3, 10);
	}
    
	//For Order timing on top
	public float GetSecondsToComplete() {
        return m_SecondsToComplete;
	}
	#endregion

	#region UserAction
	public void PlacedIngredient() {
        SetIngredientColor(m_Ingredients[m_Level]);
		m_Level--;
        if (m_Level > -1) { 
            m_Ingredients[m_Level].gameObject.SetActive(true);
        }
    }
    
    void SetIngredientColor(Ingredient ingredient)
    {
        ingredient.SetSolidAlpha();
    }
	#endregion
}

