using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {

	//Food Holder
	[SerializeField]
	GameObject[] m_FoodHolderObject;
    FoodHolder[] m_FoodHolders = new FoodHolder[4];
    FoodTimer[] m_FoodTimers = new FoodTimer[4];

    #region getter/setter
    public FoodHolder[] GetFoodHolder()
    {
        return m_FoodHolders;
    }

    Ingredient GetIngredientOnTop(Food food)
    {
        return food.GetNeededIngredient();
    }

    public void DecrementOrderTimer(Direction dir, float seconds)
    {
        m_FoodTimers[(int)dir].DecrementTimeBy(seconds);
    }
    #endregion

    #region Mono
    void Awake() {
		InitializeFoodHolders ();
	}

	void InitializeFoodHolders() {
        for (int i = 0; i < m_FoodHolderObject.Length; i++) {
			m_FoodHolders [i] = m_FoodHolderObject [i].GetComponent<FoodHolder> ();
            m_FoodTimers[i] = m_FoodHolderObject[i].GetComponent<FoodTimer>();
		}
	}

    public void UpdateFoodTimer(float seconds)
    {
        for(int i = 0; i < m_FoodTimers.Length; i++)
        {
            m_FoodTimers[i].UpdateTimer(seconds);
        }
    }
	#endregion

	#region SetupIngredients
	public void InsertFoodIntoHolder(Food food, Direction dir) {
        //Create New food - instantiate;
        Food generatedFood = InstantiateFoodInHolder(food, (int)dir);
        //Store
        m_FoodHolders[(int)dir].SetStoredFood(generatedFood);
    }
	#endregion

	#region PlayerActions
	//Place all filled holders into an array , randomly choose
    public Ingredient RandomlyChooseIngredient()
    {
		if (IsAllEmptyHolders()) {return null;}
		Food food;
		do{
			int index = Random.Range(0, 4);
			food = m_FoodHolders[index].GetStoredFood();
		} while (food == null || !food.InPlay());
        
		return GetIngredientOnTop(food);
    }

	bool IsAllEmptyHolders(){
		for (int i = 0; i < m_FoodHolders.Length; i++) {
			if (m_FoodHolders [i].GetStoredFood () != null) {
				return false;
			}
		}
		return true;
	}

    public Ingredient UserSwiped(Ingredient ingredient, Direction dir)
    {
        if (IsIngredientMatch(dir, ingredient))
        {
            m_FoodHolders[(int)dir].CorrectlySwiped();
        } else
        {
			m_FoodHolders [(int)dir].IncorrectlySwiped ();
        }
		return RandomlyChooseIngredient();
    }

    bool IsIngredientMatch(Direction dir, Ingredient swiped)
    {
        Food food = m_FoodHolders[(int)dir].GetStoredFood();
        if (food != null)
        {
            bool namesEqual = food.GetNeededIngredient().Get_IngredientName() == swiped.Get_IngredientName();
            return food.InPlay() && namesEqual;
        }
        return false;
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
