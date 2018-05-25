using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHolder : MonoBehaviour {

	public Direction dir;
	Food m_StoredFood;

	bool m_IsEmpty = true;

	public bool IsEmpty(){
		return m_IsEmpty;
	}

	public void SetStoredFood(Food food) {
		m_StoredFood = food;
	}

	public Food GetStoredFood(){
		return m_StoredFood;
	}
}
