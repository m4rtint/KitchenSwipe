using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {

	public delegate void IngredientGeneratorDelegate(Direction dir);
	public IngredientGeneratorDelegate thisDelegate;

	#region SetupIngredients
	public void SetupIngredientStack(Food[] foods){
		for (int i = 0; i < foods.Length; i++) {
//			PlaceFoodInStack (foods [i], GetStackFromIndex (i));
//			InstantiateFoodInHolder(foods, i);
		}
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
			Destroy (food.gameObject);
			//New Food creation
			thisDelegate(dir);
		} 
	}

	public void WrongSwiped() {
		//TODO - buzzer wrong sound? 

	}
	#endregion
}
