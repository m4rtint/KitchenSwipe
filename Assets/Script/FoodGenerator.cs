using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
	//Food Holder
	[SerializeField]
	GameObject m_LeftHolder;
	[SerializeField]
	GameObject m_RightHolder;
	[SerializeField]
	GameObject m_UpHolder;
	[SerializeField]
	GameObject m_DownHolder;


	[SerializeField]
	Food[] m_Foods;

	//There are four different sides
	Food[] m_ChosenFood = new Food[4];

	#region Generation
	public void ChooseFourRandomFood()
	{
		for (int i = 0; i < 4; i++)
		{
			ChooseRandomFood(i);
			PlaceFoodInHolder(i);
		}
	}

	void ChooseRandomFood(int index)
	{
		do
		{
			int randomFoodIndex = Random.Range(0, m_Foods.Length);
			m_ChosenFood[index] = m_Foods[randomFoodIndex];

		} while (IsChosenFoodAlreadyIn(index));
	}

	bool IsChosenFoodAlreadyIn(int index)
	{
		return false;
        //DEBUG
		if (m_ChosenFood.Length <= 1) { return false; }

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

	#region 
	void PlaceFoodInHolder(int index) {
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
		Instantiate(m_ChosenFood[index].gameObject,holder.transform);
        
	}
	#endregion


	#region Debug
	public void DebugPrintEachSide() {
		for (int i = 0; i < 4; i++){
			Debug.Log((Direction)i + " : " + m_ChosenFood[i].name);
		}
	}
    
	#endregion


}
