using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
	[SerializeField]
	Food[] m_Foods;

	Stack<Food> m_ChosenFoodStack = new Stack<Food>();

	public Stack<Food> GetChosenFood() {
		return m_ChosenFoodStack;	
	}

	#region Generation
	public void FillStackWithRandomFood(int num)
	{
		for (int i = 0; i < num; i++)
		{
			ChooseRandomFood();
		}
	}

	public void ChooseRandomFood()
	{
		//Choose random
        int randomFoodIndex = Random.Range(0, m_Foods.Length);
		//Store
		m_ChosenFoodStack.Push (m_Foods[randomFoodIndex]);
	
	}

	#endregion

}
