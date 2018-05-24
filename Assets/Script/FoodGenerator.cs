using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
	[SerializeField]
	Food[] m_Foods;

	//Food Holder
	[SerializeField]
	GameObject m_LeftHolder;
	[SerializeField]
	GameObject m_RightHolder;
	[SerializeField]
	GameObject m_UpHolder;
	[SerializeField]
	GameObject m_DownHolder;

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
		//Choose random
        int randomFoodIndex = Random.Range(0, m_Foods.Length);

		//Instantiate
		Food generatedFood = InstantiateFoodInHolder (m_Foods [randomFoodIndex],(int)dir);
		generatedFood.SetupIngredientLevel ();

		//Store
		m_ChosenFood[(int)dir] = generatedFood;
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

	#region Instantiate
	Food InstantiateFoodInHolder(Food food, int index) {
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
		GameObject generatedFood = Instantiate(food.gameObject,holder.transform);
		return generatedFood.GetComponent<Food> ();

	}
	#endregion
}
