using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
	[SerializeField]
	Food[] m_Foods;

	Stack<Food> m_ChosenFoodStack = new Stack<Food>();
   
	public Stack<Food> ChosenFoodStack() {
		return m_ChosenFoodStack;	
	}

    public Food firstFoodOnStack()
    {
        return ChosenFoodStack().Pop();
    }

    public Food peekFoodOnStack()
    {
        return ChosenFoodStack().Peek();
    }
    

	#region Generation
	public void fillStackWithRandomFood(int num)
	{
		for (int i = 0; i < num; i++)
		{
			chooseRandomFood();
		}
	}

	public void chooseRandomFood()
	{
		//Choose random
        int randomFoodIndex = Random.Range(0, m_Foods.Length);
		//Store
		m_ChosenFoodStack.Push (m_Foods[randomFoodIndex]);
	}

	#endregion

}
