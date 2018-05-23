using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour {

	public static DebugManager instance = null;

	[SerializeField]
	GameObject m_ListOfFood;
	[SerializeField]
	GameObject m_Top;
	[SerializeField]
	GameObject m_Bottom;
	[SerializeField]
	GameObject m_Left;
	[SerializeField]
	GameObject m_Right;
	[SerializeField]
	GameObject m_Center;

	void Awake() {
		instance = this;
	}

	public void SetListOfFood(string s){
		m_ListOfFood.GetComponent<Text> ().text = s;
	}

	public void SetTop(string s){
		m_Top.GetComponent<Text> ().text = s;
	}

	public void SetBottom(string s){
		m_Bottom.GetComponent<Text> ().text = s;
	}

	public void SetLeft(string s){
		m_Left.GetComponent<Text> ().text = s;
	}

	public void SetRight(string s){
		m_Right.GetComponent<Text> ().text = s;
	}

	public void SetCenter(string s) {
		m_Center.GetComponent<Text> ().text = s;
	}


}
