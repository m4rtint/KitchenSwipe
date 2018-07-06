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

        SetupDelegate ();
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
            GetNewIngredients();
        }
	}

    void SetupOrders()
    {
        for (int i = 0; i < 5; i++)
        {
            GetNewOrder();
        }
    }

	void SetupDelegate(){
		m_PlayerInput.GetComponent<UserInput> ().thisDelegate += PlayerSwiped;
        foreach(FoodHolder holder in m_IngredientsGenerator.GetFoodHolder())
        {
            holder.OrderDelegate += CompleteOrder;
			holder.OrderTimerDelegate += IncorrectlySwipeIngredient;
        }
	}

	void Update() {
		RunDownOrderTimer (Time.deltaTime);
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
        DebugManager.instance.SetCenter (m_CurrentIngredient.GetColor());
	}

    void GetNewIngredients()
    {
        if (m_FoodOrderManager.GetFoodQueue().Count > 0)
        {
            m_IngredientsGenerator.InsertFoodIntoHolder(m_FoodOrderManager.GetFoodQueue().Dequeue());
        }
    }
    #endregion

    #region Orders
    public virtual void CompleteOrder(Food food)
    {
		m_FoodOrderManager.RemoveFoodOrder(food);
        GetNewOrder();
        GetNewIngredients();
    }

	void IncorrectlySwipeIngredient(Food food)
	{
		m_FoodOrderManager.DecrementOrderTimer (food, TimeManager.instance.OrderPenaltyTime());
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
