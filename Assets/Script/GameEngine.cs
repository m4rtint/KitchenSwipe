using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
public class GameEngine : MonoBehaviour {

	//========================Local
    Ingredient currentIngredient;

	[SerializeField]
	int m_NumberOfFood;

    //========================Dependencies
	//Food Generator
	protected FoodGenerator m_FoodGenerator;

	//Ingredients Generator
	[SerializeField]
	GameObject m_FoodHolder;
	IngredientGenerator m_IngredientsGenerator;

	//Player Input
	[SerializeField]
	GameObject m_PlayerInput;

    //Center
    [SerializeField]
    GameObject m_Center;

    #region getter/setter
    protected int NumberOfFood() {
        return m_NumberOfFood;
    }

    protected void NumberOfFood(int size){
        m_NumberOfFood = size;
    }
    #endregion

    #region Mono
    // Use this for initialization
    void Awake () {
		m_FoodGenerator = GetComponent<FoodGenerator>();
		m_IngredientsGenerator = m_FoodHolder.GetComponent<IngredientGenerator> ();

        SetupHolders ();
	}

	protected virtual void Start()
	{
        //Set State
        StateManager.instance.startGame();

        //Get Stack of Food
		m_FoodGenerator.FillStackWithRandomFood(m_NumberOfFood);

        //Place First 4 food onto each side
        SetupIngredients();

        //Choose a random current ingredient
        ChooseNewCurrentIngredient ();
	}
	void SetupIngredients() {
		for(int i = 0; i < 4; i++)
        {
            SetNewFood((Direction)i);
        }
	}

	void SetupHolders(){
		m_PlayerInput.GetComponent<UserInput> ().swipeDelegate += PlayerSwiped;
        FoodHolder[] holders = m_IngredientsGenerator.GetFoodHolder();
        for (int i = 0; i < holders.Length; i++)
        {
            holders[i].SetDirection(i);
            holders[i].OrderDelegate += CompleteOrder;
            holders[i].OrderTimerDelegate += IncorrectlySwipeIngredient;
        }
	}

	void Update() {
		if (StateManager.instance.isInGame ()) {
			RunDownOrderTimer (Time.deltaTime * TimeManager.instance.orderTimeVaryingSpeed);
		}
	}

	void RunDownOrderTimer(float seconds) {
        //Decrement all timer of food
        m_IngredientsGenerator.UpdateFoodTimer(seconds);
    }
	#endregion

	#region Ingredients
	void ChooseNewCurrentIngredient(){
		currentIngredient = m_IngredientsGenerator.RandomlyChooseIngredient ();
		SetCenterIngredientView ();
	}

	void SetCenterIngredientView() {
        m_Center.GetComponent<CenterIngredient>().SetCenter (currentIngredient);
	}

    void SetNewFood(Direction dir)
    {
        m_IngredientsGenerator.InsertFoodIntoHolder(m_FoodGenerator.GetChosenFoodStack().Pop(),dir);
    }
    #endregion

    #region Orders
    protected virtual void CompleteOrder(Direction dir)
    {
        SetNewFood(dir);
    }

	void IncorrectlySwipeIngredient(Direction dir)
	{
        m_IngredientsGenerator.DecrementOrderTimer (dir, TimeManager.instance.FoodPenaltyTime());
	}
    #endregion

    #region Actions
    void PlayerSwiped(Direction dir)
    {
        currentIngredient = m_IngredientsGenerator.UserSwiped(currentIngredient, dir);
        SetCenterIngredientView();
    }

	#endregion

}
