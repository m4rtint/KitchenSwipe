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
            ingredient.SetAlpha(0.7f);
            ingredient.gameObject.SetActive(false);
        }
        m_Ingredients[0].gameObject.SetActive(true);
    }
    #endregion

    #region Getter/Setter
    public int MaxIngredientLevel()
    {
        return GetIngredients().Length - 1;
    }

    public Ingredient[] GetIngredients() {
		return m_Ingredients;
	}

	public Ingredient GetNeededIngredient() {
        if (!IsFoodInPlay()){ Debug.Log("Error - Tried to access index out of bounds Ingredient");}

		m_Level = Mathf.Min (MaxIngredientLevel(), m_Level);
		return m_Ingredients [m_Level];
	}

    public bool IsFoodInPlay()
    {
        return m_Level <= MaxIngredientLevel();
    }

	void SetupIngredientLevel() {
		m_Level = 0;
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
        m_Level++;
    }

    public void activeTopIngredientIfNeeded(){
        if (IsFoodInPlay())
        {
            GetNeededIngredient().gameObject.SetActive(true);
        }
    }

   
	#endregion
}

