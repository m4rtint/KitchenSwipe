using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {

	public delegate void IngredientGeneratorDelegate(Direction dir);
	public IngredientGeneratorDelegate thisDelegate;

	//Food Holder
	[SerializeField]
	GameObject m_LeftHolder;
	[SerializeField]
	GameObject m_RightHolder;
	[SerializeField]
	GameObject m_UpHolder;
	[SerializeField]
	GameObject m_DownHolder;

	//Chosen Ingredients setup to be chosen for the puzzle
	Ingredient[] Ingredients = new Ingredient[4];

	#region SetupIngredients
	public void SetupIngredientStack(Food[] foods){
		for (int i = 0; i < foods.Length; i++) {
//			PlaceFoodInStack (foods [i], GetStackFromIndex (i));
			InstantiateFoodInHolder(foods, i);
		}
	}
	#endregion


	#region Holder
	void InstantiateFoodInHolder(Food[] foods, int index) {
		GameObject holder;
		switch(index){
		case 0:
			holder = m_LeftHolder;
			break;
		case 1:
			holder = m_RightHolder;
			break;
		case 2:
			holder = m_UpHolder;
			break;
		case 3:
			holder = m_DownHolder;
			break;
		default:
			holder = m_LeftHolder;
			break;
		}
		Instantiate(foods[index].gameObject,holder.transform);

	}
	#endregion

	#region PlayerActions
	public Ingredient RandomlyChooseIngredient(Food[] food){
		int index = Random.Range (0, 3);
		return GetIngredientOnTop (food[index]);
	}

	public Ingredient GetIngredientOnTop(Food food) {
		return food.GetNeededIngredient();
	}
		
	public void CorrectlySwiped(Direction dir, Food food){
		food.PlacedIngredient();
		if (food.GetIngredientLevel() == -1) {
			//New Food creation
			thisDelegate(dir);
		} 
	}

	public void WrongSwiped() {
		//TODO - buzzer wrong sound? 

	}
	#endregion
}
