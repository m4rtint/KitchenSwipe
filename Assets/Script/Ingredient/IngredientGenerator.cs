using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {

	//Food Holder
	[SerializeField]
    GameObject[] foodHolderObjects;
    FoodHolder[] foodHolders;
    FoodTimer[] foodTimers;

    #region getter/setter
    public int foodHolderLength()
    {
        return foodHolderObjects.Length;
    }

    public FoodHolder[] FoodHolders()
    {
        return foodHolders;
    }
    #endregion

    #region Mono
    void Awake() {
        initializeFoodHolders ();
	}

	void initializeFoodHolders() {
        foodHolders = new FoodHolder[foodHolderLength()];
        foodTimers = new FoodTimer[foodHolderLength()];
        for (int i = 0; i < foodHolderObjects.Length; i++) {
			foodHolders [i] = foodHolderObjects [i].GetComponent<FoodHolder> ();
            foodTimers[i] = foodHolderObjects[i].GetComponent<FoodTimer>();
		}
	}

	#endregion

	#region PlayerActions
	//Place all filled holders into an array , randomly choose
    public Ingredient randomlyChooseIngredient(Direction dir = Direction.Down)
    {
        Ingredient ingredient = null;
        if (!isEmptyHolders()) {
            Food food = null;
            do
            {
			    int index = Random.Range(0, foodHolderLength());
			    food = foodHolders[index].StoredFood();
            } while (food == null || !food.isFoodInPlay());

            ingredient = ingredientOnTop(food);
        }
        return ingredient;
    }

    public void decrementOrderTimer(Direction dir, float seconds)
    {
        foodTimers[(int)dir].decrementTimeBy(seconds);
    }

    public Ingredient userSwiped(Ingredient ingredient, Direction dir)
    {
        if (isIngredientMatch(dir, ingredient))
        {
            foodHolders[(int)dir].correctlySwiped();
        } else
        {
			foodHolders [(int)dir].incorrectlySwiped ();
        }
		return randomlyChooseIngredient(dir);
    }

    public void insertFoodIntoHolder(Food food, Direction dir)
    {
        //Create New food - instantiate;
        Food generatedFood = instantiateFoodInHolder(food, (int)dir);
        //Store
        foodHolders[(int)dir].StoredFood(generatedFood);
    }

    public void updateFoodTimer(float seconds)
    {
        for (int i = 0; i < foodTimers.Length; i++)
        {
            foodTimers[i].updateTimer(seconds);
        }
    }
    #endregion

    #region helper
    Ingredient ingredientOnTop(Food food)
    {
        return food.GetNeededIngredient();
    }

    bool isIngredientMatch(Direction dir, Ingredient swiped)
    {
        Food food = foodHolders[(int)dir].StoredFood();
        if (food != null)
        {
            bool namesEqual = food.GetNeededIngredient().IngredientName() == swiped.IngredientName();
            return food.isFoodInPlay() && namesEqual;
        }
        return false;
    }

    bool isEmptyHolders()
    {
        for (int i = 0; i < foodHolders.Length; i++)
        {
            if (foodHolders[i].StoredFood() != null)
            {
                return false;
            }
        }
        return true;
    }

    #endregion

    #region Instantiate
    Food instantiateFoodInHolder(Food food, int index) {
		GameObject holder = foodHolderObjects[index];
		GameObject generatedFood = Instantiate (food.gameObject, holder.transform);
		return generatedFood.GetComponent<Food> ();
	}
	#endregion
}
