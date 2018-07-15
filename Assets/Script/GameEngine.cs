using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
public class GameEngine : MonoBehaviour {

	//========================Local
	Ingredient m_CurrentIngredient;

	[SerializeField]
	int m_NumberOfFood;

    //========================Dependencies
    //Food Orders
    [SerializeField]
    GameObject m_FoodOrdersObject;
    FoodOrdersManager m_FoodOrderManager;

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
        m_FoodOrderManager = m_FoodOrdersObject.GetComponent<FoodOrdersManager>();

        SetupHolders ();
	}

	protected virtual void Start()
	{
        //Get Stack of Food
		m_FoodGenerator.FillStackWithRandomFood(m_NumberOfFood);

        //Get New Orders
        SetupOrders();

        //Place First 4 food onto each side
        SetupIngredients();

        //Choose a random current ingredient
        ChooseNewCurrentIngredient ();
	}
	void SetupIngredients() {
		for(int i = 0; i < 4; i++)
        {
            GetNewIngredients((Direction)i);
        }
	}

    void SetupOrders()
    {
        for (int i = 0; i < 4; i++)
        {
            GetNewOrder();
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
		if (StateManager.instance.InGame ()) {
			RunDownOrderTimer (Time.deltaTime * TimeManager.instance.m_OrderTimeVaryingSpeed);
		}
	}

	void RunDownOrderTimer(float seconds) {
        //Decrement all timer of food
        m_FoodOrderManager.UpdateOrders(seconds);
    }
	#endregion

	#region Ingredients
	void ChooseNewCurrentIngredient(){
		m_CurrentIngredient = m_IngredientsGenerator.RandomlyChooseIngredient ();
		SetCenterIngredientView ();
	}

	void SetCenterIngredientView() {
        m_Center.GetComponent<CenterIngredient>().SetCenter (m_CurrentIngredient);
	}

    void GetNewIngredients(Direction dir)
    {
        m_IngredientsGenerator.InsertFoodIntoHolder(m_FoodOrderManager.GetFoodFromOrder(dir));
    }
    #endregion

    #region Orders
    protected virtual void CompleteOrder(Direction dir)
    {
		m_FoodOrderManager.RemoveFoodOrder(dir);
        GetNewOrder();
        GetNewIngredients(dir);
    }

	void IncorrectlySwipeIngredient(Direction dir)
	{
		m_FoodOrderManager.DecrementOrderTimer (dir, TimeManager.instance.OrderPenaltyTime());
	}

    void GetNewOrder()
    {
        if (m_FoodGenerator.GetChosenFoodStack().Count > 0)
        {
            m_FoodOrderManager.InsertFoodOrder(m_FoodGenerator.GetChosenFoodStack().Pop());
        }
    }
    #endregion

    #region Actions
    void PlayerSwiped(Direction dir)
    {
        m_CurrentIngredient = m_IngredientsGenerator.UserSwiped(m_CurrentIngredient, dir);
        SetCenterIngredientView();
    }

	#endregion

}
