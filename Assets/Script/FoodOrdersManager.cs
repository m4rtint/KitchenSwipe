using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrdersManager : MonoBehaviour
{

	[SerializeField]
	GameObject[] FoodOrdersObjects;
	FoodOrdered[] m_FoodOrders = new FoodOrdered[5];

	#region Mono
	void Awake()
	{
		SetupList ();
	}

	void SetupList() {
		m_FoodOrders = new FoodOrdered[FoodOrdersObjects.Length];
		for (int i = 0; i < FoodOrdersObjects.Length; i++) {
			m_FoodOrders [i] = FoodOrdersObjects [i].GetComponent<FoodOrdered> ();
			m_FoodOrders [i].SetIsEmpty (true);
		}
	}
	#endregion

	#region Food
	public void InsertFoodOrder(Food food) {
		for (int i = 0; i < FoodOrdersObjects.Length; i++) {
			if (m_FoodOrders [i].GetIsEmpty()) {
				m_FoodOrders [i].SetFoodId (food.GetFoodName ());
				m_FoodOrders [i].SetIsEmpty (false);
			}
		}
	}

	#endregion

}
