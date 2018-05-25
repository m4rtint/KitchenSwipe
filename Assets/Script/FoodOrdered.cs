using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrdered : MonoBehaviour{
    
	string m_FoodId;
	bool isEmpty = true;
	float m_SecondsForComplete;

	public void SetSecondsForComplete(float seconds) {
		m_SecondsForComplete = seconds;
	}

	public void DecrementSecondsBy(float seconds) {
		m_SecondsForComplete -= seconds;
	}

	public string GetFoodId() {
		return m_FoodId;
	}

	public void SetFoodId(string id){
		m_FoodId = id;
	}

	public bool GetIsEmpty() {
		return isEmpty;
	}

	public void SetIsEmpty(bool empty) {
		isEmpty = empty;
	}

}
