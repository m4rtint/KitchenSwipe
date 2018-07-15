using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrdersManager : MonoBehaviour
{

	[SerializeField]
	GameObject[] FoodOrdersObjects;
	FoodOrdered[] m_FoodOrders;
    
    #region getter/setter
    public Food GetFoodFromOrder(Direction dir)
    {
        return m_FoodOrders[(int)dir].GetFood();
    }
    #endregion

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
        foreach(FoodOrdered order in m_FoodOrders)
        {
            if (order.IsEmpty())
            {
                order.SetFood(food);
                return;
            }
        }
	}

	public void RemoveFoodOrder(Direction dir) {
        m_FoodOrders[(int)dir].RemoveFood();
	}

	//To find the order with the longest amount of time.
	//And decrement "seconds" from the order
	public void DecrementOrderTimer(Direction dir, float seconds) {
		m_FoodOrders[(int)dir].DecrementSecondsBy (seconds);
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
