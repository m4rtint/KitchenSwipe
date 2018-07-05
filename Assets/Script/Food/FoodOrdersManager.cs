using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrdersManager : MonoBehaviour
{

	[SerializeField]
	GameObject[] FoodOrdersObjects;
	FoodOrdered[] m_FoodOrders;

    Queue<Food> m_FoodQueue = new Queue<Food>();
    #region getter/setter
    public Queue<Food> GetFoodQueue()
    {
        return m_FoodQueue;
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
                m_FoodQueue.Enqueue(food);
                return;
            }
        }
	}

	//To find the order with the shortest amount of time.
	//And remove the food from the orders
	public void RemoveFoodOrder(Food food) {
        float timeUntilComplete = float.MaxValue;
        FoodOrdered chosenOrder = null;
        foreach(FoodOrdered order in m_FoodOrders)
        {
            Food orderFood = order.GetFood();

            if (orderFood != null && 
                orderFood.GetFoodName() == food.GetFoodName() &&
                orderFood.GetSecondsToComplete() < timeUntilComplete) 
            {
                chosenOrder = order;
				timeUntilComplete = order.GetSecondsForComplete ();
            }
        }

        if (chosenOrder != null)
        {
            chosenOrder.RemoveFood();
        }
	}

	//To find the order with the longest amount of time.
	//And decrement "seconds" from the order
	public void DecrementOrderTimer(Food food, float seconds) {
		float timeUntilComplete = float.MinValue;
		FoodOrdered chosenOrder = null;
		foreach(FoodOrdered order in m_FoodOrders)
		{
			if (!order.IsEmpty() && 
				order.GetFood().GetFoodName() == food.GetFoodName() &&
				order.GetSecondsForComplete() > timeUntilComplete) 
			{
				chosenOrder = order;
				timeUntilComplete = order.GetSecondsForComplete ();
			}
		}

		if (chosenOrder != null)
		{
			chosenOrder.DecrementSecondsBy (seconds);
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
