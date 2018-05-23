using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {

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
	Stack[] Ingredients;
	Stack<Ingredient> m_UpStack = new Stack<Ingredient>();
	Stack<Ingredient> m_LeftStack= new Stack<Ingredient>();
	Stack<Ingredient> m_BottomStack= new Stack<Ingredient>();
	Stack<Ingredient> m_RightStack= new Stack<Ingredient>();

	#region SetupIngredients
	public void SetupIngredientStack(Food[] foods){
		for (int i = 0; i < foods.Length; i++) {
			PlaceFoodInStack (foods [i], GetStackFromIndex (i));
			InstantiateFoodInHolder(foods, i);
		}
	}
	#endregion

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
	public Ingredient RandomlyChooseIngredient(){
		int index = Random.Range (0, 3);
		return GetStackFromIndex (index).Peek ();
	}


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
