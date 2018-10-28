using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
public class GameEngine : MonoBehaviour {

	//========================Local
    protected Ingredient currentIngredient;

	[SerializeField]
	int numberOfFood;

    //========================Dependencies
	//Food Generator
	protected FoodGenerator foodGenerator;

	//Ingredients Generator
	[SerializeField]
	GameObject m_FoodHolder;
	protected IngredientGenerator ingredientsGenerator;

    //Center
    [SerializeField]
    GameObject center;
    [SerializeField]
    UserInput userInput;

    //Game Timer
    [SerializeField]
    GameTimer gameTimer;

    #region getter/setter
    protected int NumberOfFood() {
        return numberOfFood;
    }

    protected void NumberOfFood(int size){
        numberOfFood = size;
    }
    #endregion

    #region Mono
    // Use this for initialization
    protected virtual void Awake () {
		foodGenerator = GetComponent<FoodGenerator>();
		ingredientsGenerator = m_FoodHolder.GetComponent<IngredientGenerator> ();

        setupHolders ();
    }

	protected virtual void Start()
	{
        setupDelegates();

        //Get Stack of Food
        foodGenerator.fillStackWithRandomFood(numberOfFood);

        //Place First 4 food onto each side
        setupIngredients();

        //Choose a random current ingredient
        ChooseNewCurrentIngredient ();
	}

    //Start Setup Only
    void ChooseNewCurrentIngredient()
    {
        currentIngredient = ingredientsGenerator.randomlyChooseIngredient();
        setCenterIngredientView();
    }

    protected void setupIngredients() {
        for (int i = 0; i < ingredientsGenerator.foodHolderLength(); i++)
        {
            setNewFood((Direction)i);
        }
	}

    protected virtual void setupDelegates()
    {
        TimeManager.instance.isGameOverDelegate += onGameOver;
    }

    void setupHolders(){
        userInput.swipeDelegate += playerSwiped;
        FoodHolder[] holders = ingredientsGenerator.FoodHolders();
        for (int i = 0; i < holders.Length; i++)
        {
            holders[i].Direction(i);
            holders[i].orderDelegate += CompleteOrder;
            holders[i].orderTimerDelegate += incorrectlySwipeIngredient;
            holders[i].FoodTimer().foodTimerRanOutDelegate += gameTimer.shakeRedText;
        }
	}

	protected virtual void Update() {
		if (StateManager.instance.isInGame ()) {
			RunDownOrderTimer (Time.deltaTime * TimeManager.instance.orderTimeVaryingSpeed);
		}
	}

	void RunDownOrderTimer(float seconds) {
        //Decrement all timer of food
        ingredientsGenerator.updateFoodTimer(seconds);
    }
    #endregion

    #region Ingredients
    protected void setCenterIngredientView()
    {
        center.GetComponent<CenterIngredient>().SetCenter(currentIngredient);
    }

    void setNewFood(Direction dir)
    {
        ingredientsGenerator.insertFoodIntoHolder(foodGenerator.firstFoodOnStack(),dir);
    }
    #endregion

    #region Orders
    protected virtual void CompleteOrder(Direction dir)
    {
        setNewFood(dir);
    }

    protected virtual void incorrectlySwipeIngredient(Direction dir)
	{
        ingredientsGenerator.decrementOrderTimer (dir, TimeManager.instance.FoodPenaltyTime());
	}
    #endregion

    #region Actions
    protected virtual void playerSwiped(Direction dir)
    {
        currentIngredient = ingredientsGenerator.userSwiped(currentIngredient, dir);
        if (currentIngredient == null)
        {
            currentIngredient = foodGenerator.peekFoodOnStack().GetNeededIngredient();
        }
        setCenterIngredientView();
    }

    #endregion

    #region Delegate
    protected virtual void onGameOver()
    {
        StateManager.instance.gameOver();
        ScoreManager.instance.saveScore();
    }
    #endregion
}
