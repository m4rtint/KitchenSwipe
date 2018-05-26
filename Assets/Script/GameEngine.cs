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
            Food chosenFood = m_FoodGenerator.GetChosenFood().Pop();
            m_FoodOrderManager.InsertFoodOrder(chosenFood);
            m_IngredientsGenerator.InsertFoodIntoHolder (chosenFood);
		}
	}

	void SetupDelegate(){
		m_PlayerInput.GetComponent<UserInput> ().thisDelegate += PlayerSwiped;
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
		DebugManager.instance.SetCenter (m_CurrentIngredient.Get_IngredientName ());
	}
	#endregion

	#region Actions
	void PlayerSwiped(Direction dir) {
		Food food = m_IngredientsGenerator.GetFoodFromHolder(dir);
		if (IsIngredientMatch(m_CurrentIngredient, m_IngredientsGenerator.GetIngredientOnTop(food))) {
			//It is the same ingredient
			m_IngredientsGenerator.CorrectlySwiped(dir, food);
		} else {
			//Wrong ingredient
			m_IngredientsGenerator.WrongSwiped();
		}
        //Update each side with new ingredients
		//DebugManager.instance.DebugPrintEachSide (m_FoodGenerator.GetChosenFood());

		ChooseNewCurrentIngredient ();
	}

	bool IsIngredientMatch(Ingredient answer, Ingredient swiped) {
		return answer.Get_IngredientName() == swiped.Get_IngredientName();
	}

	#endregion

}
