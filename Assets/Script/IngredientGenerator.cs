using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {

	Stack[] Ingredients;

	Stack<Ingredient> m_UpStack = new Stack<Ingredient>();
	Stack<Ingredient> m_LeftStack= new Stack<Ingredient>();
	Stack<Ingredient> m_BottomStack= new Stack<Ingredient>();
	Stack<Ingredient> m_RightStack= new Stack<Ingredient>();


	public void SetupIngredientStack(Food[] foods){
		for (int i = 0; i < foods.Length; i++) {
			PlaceFoodInStack (foods [i], GetStackFromIndex (i));
		}
	}

	public Ingredient RandomlyChooseIngredient(){
		int index = Random.Range (0, 3);
		return GetStackFromIndex (index).Peek ();
	}

	#region Stacks
	Stack<Ingredient> GetStackFromIndex(int foodIndex) {
		if (foodIndex == 0) {
			return m_LeftStack;
		} else if (foodIndex == 1) {
			return m_RightStack;
		} else if (foodIndex == 2) {
			return m_UpStack;
		} else {
			return m_BottomStack;
		}
	}
			
	void PlaceFoodInStack(Food food, Stack<Ingredient> stack) {
		for (int i = 0; i < food.GetIngredients ().Length; i++) {
			stack.Push (food.GetIngredients () [i]);
		}
	}

	public Ingredient GetIngredientOnStack(int index) {
		return GetStackFromIndex (index).Peek ();
	}
	#endregion

	#region PlayerActions

	//
	public void CorrectlySwiped(Direction dir){
		//Pop Ingredient
		//If 0 - choose random food and place it back into stack/array
		Stack<Ingredient> currentStack = GetStackFromIndex((int)dir);
		currentStack.Pop ();
		if (currentStack.Count == 0) {
			//New food in stack

		} 
	}

	public void WrongSwiped() {

	}
	#endregion
}
