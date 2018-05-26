using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {

	//Food Holder
	[SerializeField]
	GameObject[] m_FoodHolderObject;

    FoodHolder[] m_FoodHolders;

    #region getter/setter
    public FoodHolder[] GetFoodHolder()
    {
        return m_FoodHolders;
    }
    #endregion

    #region Mono
    void Awake() {
		InitializeFoodHolders ();
	}

	void InitializeFoodHolders() {
        m_FoodHolders = new FoodHolder[m_FoodHolderObject.Length];
        for (int i = 0; i < m_FoodHolderObject.Length; i++) {
			m_FoodHolders [i] = m_FoodHolderObject [i].GetComponent<FoodHolder> ();
		}
	}
	#endregion

	#region SetupIngredients
	public void InsertFoodIntoHolder(Food food) {
		//Check which holder is free.
		for (int i = 0; i < m_FoodHolders.Length; i++) {
			if (m_FoodHolders [i].GetStoredFood() == null) {
				//Create New food - instantiate;
				Food generatedFood = InstantiateFoodInHolder(food, i);
				//Store
				m_FoodHolders [i].SetStoredFood (generatedFood);
                //View
                m_FoodHolders[i].UpdateListOfIngredientsView();
                return;
			}
		}

	}
	#endregion

	#region PlayerActions
    Food GetFoodFromHolder(Direction dir)
    {
        return m_FoodHolders[(int)dir].GetStoredFood();
    }

	Ingredient GetIngredientOnTop(Food food) {
		return food.GetNeededIngredient();
	}

    public Ingredient RandomlyChooseIngredient()
    {
        int index = Random.Range(0, 3);
        return GetIngredientOnTop(m_FoodHolders[index].GetStoredFood());
    }

    public Ingredient UserSwiped(Ingredient ingredient, Direction dir)
    {
        if (IsIngredientMatch(dir, ingredient))
        {
            //Correctly Swiped
            m_FoodHolders[(int)dir].CorrectlySwiped();
        } else
        {
            //Incorrectyl swiped
        }
        return ingredient;
    }

    bool IsIngredientMatch(Direction dir, Ingredient swiped)
    {
        return GetFoodFromHolder(dir).GetNeededIngredient().Get_IngredientName() == swiped.Get_IngredientName();
    }

    #endregion

    #region Instantiate
    Food InstantiateFoodInHolder(Food food, int index) {
		GameObject holder = m_FoodHolderObject[index];
		GameObject generatedFood = Instantiate (food.gameObject, holder.transform);
		return generatedFood.GetComponent<Food> ();
	}
	#endregion
}
