using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrdersManager : MonoBehaviour
{

	[SerializeField]
	GameObject[] FoodOrdersObjects;
	FoodOrdered[] m_FoodOrders;

	#region Mono
	void Awake()
	{
        InitializeOrderComponents();
	}

	void InitializeOrderComponents() {
		m_FoodOrders = new FoodOrdered[FoodOrdersObjects.Length];
		for (int i = 0; i < FoodOrdersObjects.Length; i++) {
			m_FoodOrders [i] = FoodOrdersObjects [i].GetComponent<FoodOrdered> ();
		}
	}
	#endregion

	#region Food
	public void InsertFoodOrder(Food food) {
		for (int i = 0; i < FoodOrdersObjects.Length; i++) {
			if (m_FoodOrders [i].GetIsEmpty()) {
				m_FoodOrders [i].SetFood (GenerateFoodId(food, i),food.GetSecondsToComplete());
                return;
			}
		}
	}

    string GenerateFoodId(Food food, int holder)
    {
        return food.GetFoodName() + holder.ToString();
    }

	public void RemoveFoodOrder(Food food, Direction dir) {
		foreach(FoodOrdered order in m_FoodOrders) {
			if (order.GetFoodId() == GenerateFoodId(food,(int)dir)) {
				order.RemoveFood();
			}
		}      
	}
    #endregion

    #region Orders
    public void UpdateOrders(float seconds)
    {
        for(int i = 0; i < m_FoodOrders.Length; i++)
        {
            m_FoodOrders[i].UpdateOrderView(seconds);
        }
    }
    #endregion

}
