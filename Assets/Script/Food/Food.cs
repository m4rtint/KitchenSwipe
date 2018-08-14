using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodAnimation))]
public class Food : MonoBehaviour {

	[SerializeField]
	Ingredient[] m_Ingredients;
	[SerializeField]
	float m_SecondsToComplete;

	int m_Level;

	#region Mono
	void Awake(){
		SetupIngredientLevel ();
        SetupIngredientColor();
	}

    void SetupIngredientColor()
    {
        foreach(Ingredient ingredient in m_Ingredients)
        {
            ingredient.SetAlpha(0.3f);
            ingredient.gameObject.SetActive(false);
        }
        m_Ingredients[m_Ingredients.Length - 1].gameObject.SetActive(true);
    }
    #endregion

    #region Getter/Setter

    public Ingredient[] GetIngredients() {
		return m_Ingredients;
	}

	public Ingredient GetNeededIngredient() {
        if (!InPlay()){ Debug.Log("Error - Tried to access -1 Ingredient");}

		m_Level = Mathf.Max (0, m_Level);
		return m_Ingredients [m_Level];
	}

	public int GetIngredientLevel() {
		return m_Level;
	}

    public bool InPlay()
    {
        return m_Level >= 0;
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
	public void PlaceIngredient() {
        GetNeededIngredient().SetAlpha(1);
        GetNeededIngredient().StartAnimation();
        m_Level--;
    }

    public void activeTopIngredientIfNeeded(){
        if (m_Level > -1)
        {
            GetNeededIngredient().gameObject.SetActive(true);
        }
    }

   
	#endregion
}

