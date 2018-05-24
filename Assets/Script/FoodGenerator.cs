using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
	[SerializeField]
	Food[] m_Foods;

	//There are four different sides
	Food[] m_ChosenFood = new Food[4];

	public Food[] GetChosenFood() {
		return m_ChosenFood;
	}

	#region Generation
	public void ChooseFourRandomFood()
	{
		for (int i = 0; i < 4; i++)
		{
			ChooseRandomFood((Direction)i);
		}
	}

	public void ChooseRandomFood(Direction dir)
	{
        int randomFoodIndex = Random.Range(0, m_Foods.Length);
		m_ChosenFood[(int)dir] = m_Foods[randomFoodIndex];
		m_ChosenFood[(int)dir].SetupIngredientLevel ();
	}

	bool IsChosenFoodAlreadyIn(int index)
	{
		if (index == 0) { return false; }

		for (int i = 0; i < index; i++)
		{
			if (m_ChosenFood[i].name == m_ChosenFood[index].name)
			{
				return true;
			}
		}
		return false;
	}
	#endregion
}
