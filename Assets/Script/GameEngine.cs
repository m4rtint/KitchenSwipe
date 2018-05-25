﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
public class GameEngine : MonoBehaviour {

	//========================Local
	Ingredient m_CurrentIngredient;

	[SerializeField]
	int m_NumberOfFood;

	//========================Dependencies
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
		SetupDelegate ();
	}

	void Start()
	{
		m_FoodGenerator.FillListWithRandomFood(m_NumberOfFood);
		//Send 4 food from stack into Ingredient Generator

		#if UNITY_EDITOR
//		DebugManager.instance.DebugPrintEachSide(m_IngredientsGenerator.getfood);
		#endif

		ChooseNewCurrentIngredient ();

	}
	void SetupIngredients() {
		for (int i = 0; i < 4; i++) {
			m_IngredientsGenerator.inser
		}
	}

	void SetupDelegate(){
		m_IngredientsGenerator.thisDelegate += m_FoodGenerator.ChooseRandomFood;
		m_PlayerInput.GetComponent<UserInput> ().thisDelegate += PlayerSwiped;
	}

	void Update() {
		RunDownOrderTimer ();
	}

	void RunDownOrderTimer() {
		//Decrement all timer of food

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
