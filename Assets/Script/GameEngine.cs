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
	FoodGenerator m_FoodGenerator;

	//Ingredients Generator
	[SerializeField]
	GameObject m_FoodHolder;
	IngredientGenerator m_IngredientsGenerator;

	//Player Input
	[SerializeField]
	GameObject m_PlayerInput;

	#region Mono
	// Use this for initialization
	void Awake () {
		m_FoodGenerator = GetComponent<FoodGenerator>();
		m_IngredientsGenerator = m_FoodHolder.GetComponent<IngredientGenerator> ();
        m_FoodOrderManager = m_FoodOrdersObject.GetComponent<FoodOrdersManager>();

        SetupDelegate ();
	}

	void Start()
	{
        //Get Stack of Food
		m_FoodGenerator.FillStackWithRandomFood(m_NumberOfFood);

        //Place First 4 food onto each side
        SetupIngredients();

        //Choose a random current ingredient
        ChooseNewCurrentIngredient ();
	}
	void SetupIngredients() {
		for (int i = 0; i < 4; i++) {
            GetRandomFood();
        }
	}

	void SetupDelegate(){
		m_PlayerInput.GetComponent<UserInput> ().thisDelegate += PlayerSwiped;
        foreach(FoodHolder holder in m_IngredientsGenerator.GetFoodHolder())
        {
            holder.thisDelegate += GetRandomFood;
            holder.holderOrderDelegate += RemoveOrders;
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
    void GetRandomFood()
    {
		if (m_FoodGenerator.GetChosenFood().Count > 0) {
            Food chosenFood = m_FoodGenerator.GetChosenFood().Pop();
            m_FoodOrderManager.InsertFoodOrder(chosenFood);
            m_IngredientsGenerator.InsertFoodIntoHolder(chosenFood);
		}
    }

	void ChooseNewCurrentIngredient(){
		m_CurrentIngredient = m_IngredientsGenerator.RandomlyChooseIngredient ();
		SetCenterIngredientView ();
	}

	void SetCenterIngredientView() {
		DebugManager.instance.SetCenter (m_CurrentIngredient.Get_IngredientName ());
	}
    #endregion

    #region Orders
    void RemoveOrders(Direction dir, Food food)
    {
        Debug.Log("Remove Orders");
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
