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

	public Food[] GetChosenFood() {
		return m_ChosenFood;
	}

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
        int randomFoodIndex = Random.Range(0, m_Foods.Length);
		m_ChosenFood[index] = m_Foods[randomFoodIndex];
	}

	bool IsChosenFoodAlreadyIn(int index)
	{
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

	#region Holder
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
		string printingLog = "                 " + m_ChosenFood [2].name + "             \n";
		printingLog += m_ChosenFood [0].name + "                            " + m_ChosenFood [1].name + "\n"; 
		string printingLog_2 = "                 " + m_ChosenFood [3].name + "             \n";
		Debug.Log (printingLog);
		Debug.Log (printingLog_2);
		Debug.Log ("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
		DebugUIEachSide ();
	}
    
	void DebugUIEachSide(){
		DebugManager.instance.SetTop (m_ChosenFood [2].name);
		DebugManager.instance.SetBottom (m_ChosenFood [3].name);
		DebugManager.instance.SetLeft (m_ChosenFood [0].name);
		DebugManager.instance.SetRight (m_ChosenFood [1].name);

		string listOfFood = "";
		for (int i = 0; i < m_ChosenFood.Length; i++) {
			listOfFood += m_ChosenFood [i].name + ",";
		}
		DebugManager.instance.SetListOfFood (listOfFood);
	}
	#endregion


}
