﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
public class GameEngine : MonoBehaviour {

	//========================Local
    protected Ingredient currentIngredient;
    [Header("Local")]
	[SerializeField]
	int numberOfFood;

    //========================Dependencies
    //Food Generator
    protected FoodGenerator foodGenerator;
    //Ingredients Generator
    [Header("Dependencies")]
    [SerializeField]
	protected IngredientGenerator ingredientsGenerator;
    [SerializeField]
    NextIngredient nextIngredient;
    [SerializeField]
    CenterIngredient center;
    [SerializeField]
    GameTimer gameTimer;

    [Header("Input")]
    [SerializeField]
    UserInput userInput;
    [SerializeField]
    TrashInput trashInput;

    #region getter/setter
    protected int NumberOfFood() {
        return numberOfFood;
    }

    protected void NumberOfFood(int size){
        numberOfFood = size;
    }

    protected GameObject Trash()
    {
        return trashInput.gameObject;
    }
    #endregion

    #region Mono
    // Use this for initialization
    protected virtual void Awake () {
		foodGenerator = GetComponent<FoodGenerator>();

        setupHolders();
        setupUserInputDelegates();
    }

	protected virtual void Start()
	{
        setupDelegates();

        //Get Stack of Food
        foodGenerator.fillStackWithRandomFood(numberOfFood);

        //Place First 4 food onto each side
        setupIngredients();

        //Choose a random current ingredient
        setNextIngredient();
        
        setNextIngredientAsCenter();
	}
    
    protected void setNextIngredientAsCenter()
    {
        currentIngredient = nextIngredient.Ingredient();
        setNextIngredient();
        setCenterIngredientView();
    }

    protected void setNextIngredient(Ingredient ingredient = null)
    {
        if (ingredient == null) {
            nextIngredient.Ingredient(ingredientsGenerator.randomlyChooseIngredient());
        } else {
            nextIngredient.Ingredient(ingredient);
        }
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

    void setupUserInputDelegates()
    {
        userInput.swipeDelegate += playerSwiped;
        trashInput.doubleTapDelegate += setNextIngredientAsCenter;
    }

    void setupHolders(){
        FoodHolder[] holders = ingredientsGenerator.FoodHolders();
        for (int i = 0; i < holders.Length; i++)
        {
            holders[i].Direction(i);
            holders[i].orderDelegate += CompleteOrder;
            holders[i].orderTimerDelegate += incorrectlySwipeIngredient;
            setupGameTimerDelegateIfNeeded(holders[i]);
        }
	}
    
    void setupGameTimerDelegateIfNeeded(FoodHolder holder)
    {
        if (gameTimer != null) { 
            holder.FoodTimer().foodTimerRanOutDelegate += gameTimer.shakeRedText;
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
        center.Center(currentIngredient);
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
        Ingredient next = ingredientsGenerator.userSwiped(currentIngredient, dir);
        if (next == null)
        {
            next = foodGenerator.peekFoodOnStack().GetNeededIngredient();
        }

        currentIngredient = nextIngredient.Ingredient();
        nextIngredient.Ingredient(next);
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
