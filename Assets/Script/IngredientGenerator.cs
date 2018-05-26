using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour {

	public delegate void IngredientGeneratorDelegate(Direction dir);
	public IngredientGeneratorDelegate thisDelegate;

	//Food Holder
	[SerializeField]
	GameObject[] m_FoodHolderObject;

    FoodHolder[] m_FoodHolders;


	#region Mono
	void Awake() {
		InitializeFoodHolders ();
	}

	void InitializeFoodHolders() {
        m_FoodHolders = new FoodHolder[m_FoodHolderObject.Length];
        for (int i = 0; i < m_FoodHolderObject.Length; i++) {
			m_FoodHolders [i] = m_FoodHolderObject [i].GetComponent<FoodHolder> ();
		}
	}
	#endregion

	#region SetupIngredients
	public void InsertFoodIntoHolder(Food food) {
		//Check which holder is free.
		for (int i = 0; i < m_FoodHolders.Length; i++) {
			if (m_FoodHolders [i].IsEmpty ()) {
				//Create New food - instantiate;
				Food generatedFood = InstantiateFoodInHolder(food, i);
				//Store
				m_FoodHolders [i].SetStoredFood (generatedFood);
                //View
                m_FoodHolders[i].UpdateListOfIngredientsView();
			}
		}

	}
	#endregion

	#region PlayerActions
    public Food GetFoodFromHolder(Direction dir)
    {
        return m_FoodHolders[(int)dir].GetStoredFood();
    }

	public Ingredient RandomlyChooseIngredient(){
		int index = Random.Range (0, 3);
		return GetIngredientOnTop (m_FoodHolders[index].GetStoredFood());
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

	#region Instantiate
	Food InstantiateFoodInHolder(Food food, int index) {
		GameObject holder = m_FoodHolderObject[index];
		GameObject generatedFood = Instantiate (food.gameObject, holder.transform);
		return generatedFood.GetComponent<Food> ();
	}
	#endregion
}
