using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
public class GameEngine : MonoBehaviour {

	Ingredient m_CurrentIngredient;
	FoodGenerator m_FoodGenerator;
	IngredientGenerator m_IngredientGenerator;

	// Use this for initialization
	void Awake () {
		m_FoodGenerator = GetComponent<FoodGenerator>();
		m_IngredientGenerator = GetComponent<IngredientGenerator> ();
	}

	private void Start()
	{
		m_FoodGenerator.ChooseFourRandomFood();
		m_FoodGenerator.DebugPrintEachSide();

		m_IngredientGenerator.SetupIngredientStack (m_FoodGenerator.GetChosenFood ());
		m_CurrentIngredient = m_IngredientGenerator.RandomlyChooseIngredient ();
	}

	#region Actions
	public void PlayerSwiped(Direction dir) {
		if (IsIngredientMatch(m_CurrentIngredient, m_IngredientGenerator.GetIngredientOnStack((int)dir))) {
			//It is the same ingredient
			m_IngredientGenerator.CorrectlySwiped(dir);
		} else {
			//Wrong ingredient
			m_IngredientGenerator.WrongSwiped();
		}
	}

	bool IsIngredientMatch(Ingredient answer, Ingredient swiped) {
		return answer.Get_IngredientName() == swiped.Get_IngredientName();
	}

	#endregion

}
