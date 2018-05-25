using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
	[SerializeField]
	Food[] m_Foods;

	Stack<Food> m_ChosenFoodStack = new Stack<Food>();

	public List<Food> GetChosenFood() {
		return m_ChosenFoodStack;	
	}

	#region Generation
	public void FillListWithRandomFood(int num)
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

	//TODO Change this
//	bool IsChosenFoodAlreadyIn(int index)
//	{
//		if (index == 0) { return false; }
//
//		for (int i = 0; i < index; i++)
//		{
//			if (m_ChosenFood[i].name == m_ChosenFood[index].name)
//			{
//				return true;
//			}
//		}
//		return false;
//	}
	#endregion

}
