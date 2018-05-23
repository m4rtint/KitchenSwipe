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

	#region Mono
	// Use this for initialization
	void Awake () {
		m_FoodGenerator = GetComponent<FoodGenerator>();
		m_IngredientsGenerator = m_FoodHolder.GetComponent<IngredientGenerator> ();
	}

	void Start()
	{
		m_FoodGenerator.ChooseFourRandomFood();

		#if UNITY_EDITOR
		DebugManager.instance.DebugPrintEachSide(m_FoodGenerator.GetChosenFood ());
		#endif

		m_IngredientsGenerator.SetupIngredientStack (m_FoodGenerator.GetChosenFood ());
		m_CurrentIngredient = m_IngredientsGenerator.RandomlyChooseIngredient ();


		DebugManager.instance.SetCenter (m_CurrentIngredient.Get_IngredientName ());

	}
	#endregion

	#region Actions
	public void PlayerSwiped(Direction dir) {
		if (IsIngredientMatch(m_CurrentIngredient, m_IngredientsGenerator.GetIngredientOnStack((int)dir))) {
			//It is the same ingredient
			m_IngredientsGenerator.CorrectlySwiped(dir);
		} else {
			//Wrong ingredient
			m_IngredientsGenerator.WrongSwiped();
		}
	}

	bool IsIngredientMatch(Ingredient answer, Ingredient swiped) {
		return answer.Get_IngredientName() == swiped.Get_IngredientName();
	}

	#endregion

}
