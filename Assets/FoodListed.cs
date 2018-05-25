using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodListed : MonoBehaviour{
    
	string m_FoodId;
	bool isEmpty = true;

	public string GetFoodId() {
		return m_FoodId;
	}

	public void SetFoodId(string id){
		m_FoodId = id;
	}

	public bool GetIsEmpty() {
		return isEmpty;
	}

}
