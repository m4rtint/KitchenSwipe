using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodOrdered : MonoBehaviour
{

	string m_FoodId;
	bool isEmpty = true;
	float m_SecondsForComplete;

	#region Setter/Getter
	public void SetSecondsForComplete(float seconds)
	{
		m_SecondsForComplete = seconds;
	}

	public bool GetIsEmpty()
	{
		return isEmpty;
	}

	public string GetFoodId()
	{
		return m_FoodId;
	}
	#endregion


	#region View
	public void RemoveFood(){
		m_FoodId = "";
		m_SecondsForComplete = 0;
		isEmpty = true;
	}

	public void UpdateOrderView(float seconds)
	{
		if (isEmpty)
        {
            SetOrderEmptyView();
            return;
        }
		DecrementSecondsBy(seconds);
		SetOrderView();
	}

	void DecrementSecondsBy(float seconds)
	{
		m_SecondsForComplete -= seconds;
	}

	public void SetFood(string id, float seconds)
	{
		m_FoodId = id;
		m_SecondsForComplete = seconds;
		isEmpty = false;
	}

	void SetOrderView()
	{
		GetComponent<Text>().text = (int)m_SecondsForComplete + "\n" + m_FoodId;
	}

	void SetOrderEmptyView() {
		GetComponent<Text>().text = "Empty";
	}
#endregion

}
