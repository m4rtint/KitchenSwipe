using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {

	//Food Holder
	[SerializeField]
    GameObject[] foodHolderObjects;
    FoodHolder[] foodHolders;
    FoodTimer[] foodTimers;

    int lastIndex;

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
    public Ingredient randomlyChooseIngredient()
    {
        Ingredient ingredient = null;
        int crashPrevention = 10;
        if (!isHoldersEmpty()) {
            Food food = null;
            int index;
            do
            {
			    index = Random.Range(0, foodHolderLength());
			    food = foodHolders[index].StoredFood();

                crashPrevention--;
                if (crashPrevention == 5)
                {
                    IEnumerator wait = waitToChoose();
                    StartCoroutine(wait);
                } else if (crashPrevention == 0)
                {
                    return null;
                }

            } while (isRechoosingIngredientNeeded(food, index));

            lastIndex = index;
            ingredient = ingredientOnTop(food);
        }
        return ingredient;
    }

    private IEnumerator waitToChoose()
    {
        yield return new WaitForSeconds(1.0f);
    }

    private bool isRechoosingIngredientNeeded(Food food, int index)
    {
        int numberOfHoldersLeft = 0;
        foreach (FoodHolder holder in this.foodHolders)
        {
            Food holdersFood = holder.StoredFood();
            if (holdersFood != null)
            {
                if (holdersFood.isFoodInPlay())
                {
                    numberOfHoldersLeft++;
                }
            }
        }

        return food == null || !food.isFoodInPlay() || (index == lastIndex && numberOfHoldersLeft > 1);
    }

    public Ingredient chooseIngredientFrom(Direction dir) {
        Ingredient ingredient = null;
        int index = (int)dir;
        if (foodHolders.Length > index) {
            Food food = foodHolders[index].StoredFood();
            if(food != null) {
                ingredient = ingredientOnTop(food);
            }
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
		return randomlyChooseIngredient();
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

    bool isIngredientMatch(Direction dir, Ingredient center)
    {
        bool result = false;
        Food sideFood = foodHolders[(int)dir].StoredFood();
        if (sideFood != null && sideFood.isFoodInPlay())
        {
            Ingredient sideIngredient = sideFood.GetNeededIngredient();
            bool namesEqual = sideIngredient.IngredientName() == center.IngredientName();
            result = sideFood.isFoodInPlay() && namesEqual;
            //ANALYTICS
            if (!result && FbAnalytics.instance != null)
            {
                FbAnalytics.instance.wrongSwipe(dir, center.IngredientName(), sideIngredient.IngredientName());
            }
        }
        return result;
    }

    public bool isHoldersEmpty()
    {
        for (int i = 0; i < foodHolders.Length; i++)
        {
            if (foodHolders[i].StoredFood() != null && foodHolders[i].StoredFood().isFoodInPlay())
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
        generatedFood.transform.SetAsFirstSibling();
		return generatedFood.GetComponent<Food> ();
	}
	#endregion
}
