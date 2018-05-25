using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
public class GameEngine : MonoBehaviour {

	Ingredient m_CurrentIngredient;
	FoodGenerator m_FoodGenerator;

	//Ingredients Holder + Generator
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
		SetupDelegate ();
	}

	void Start()
	{
		m_FoodGenerator.ChooseFourRandomFood();

		#if UNITY_EDITOR
		DebugManager.instance.DebugPrintEachSide(m_FoodGenerator.GetChosenFood ());
		#endif

		m_IngredientsGenerator.SetupIngredientStack (m_FoodGenerator.GetChosenFood ());
		ChooseNewCurrentIngredient ();

	}

	void SetupDelegate(){
		m_IngredientsGenerator.thisDelegate += m_FoodGenerator.ChooseRandomFood;
		m_PlayerInput.GetComponent<UserInput> ().thisDelegate += PlayerSwiped;
	}

	void Update() {
		//Decrement all timer of food
		for (int i = 0; m_FoodGenerator.GetChosenFood ().Length; i++) {
			m_FoodGenerator.GetChosenFood()[i].
		}
	}
	#endregion


	#region Ingredients
	void ChooseNewCurrentIngredient(){
		m_CurrentIngredient = m_IngredientsGenerator.RandomlyChooseIngredient (m_FoodGenerator.GetChosenFood());
		SetCenterIngredientView ();
	}

	void SetCenterIngredientView() {
		DebugManager.instance.SetCenter (m_CurrentIngredient.Get_IngredientName ());
	}
	#endregion

	#region Actions
	void PlayerSwiped(Direction dir) {
		Food food = m_FoodGenerator.GetChosenFood () [(int)dir];
		if (IsIngredientMatch(m_CurrentIngredient, m_IngredientsGenerator.GetIngredientOnTop(food))) {
			//It is the same ingredient
			m_IngredientsGenerator.CorrectlySwiped(dir, food);
		} else {
			//Wrong ingredient
			m_IngredientsGenerator.WrongSwiped();
		}
		DebugManager.instance.DebugPrintEachSide (m_FoodGenerator.GetChosenFood());

		ChooseNewCurrentIngredient ();
	}

	bool IsIngredientMatch(Ingredient answer, Ingredient swiped) {
		return answer.Get_IngredientName() == swiped.Get_IngredientName();
	}

	#endregion

}
