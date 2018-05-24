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

	public bool C_SideFood;
	public bool C_Direction;

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

	public void DebugPrintEachSide(Food[] m_ChosenFood) {
		if (C_SideFood) {
			string printingLog = "                 " + m_ChosenFood [2].name + "             \n";
			printingLog += m_ChosenFood [0].name + "                            " + m_ChosenFood [1].name + "\n"; 
			string printingLog_2 = "                 " + m_ChosenFood [3].name + "             \n";
			Debug.Log (printingLog);
			Debug.Log (printingLog_2);
			Debug.Log ("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
		}
		DebugUIEachSide (m_ChosenFood);
	}

	void DebugUIEachSide(Food[] m_ChosenFood){
		SetTop (ListOfIngredients(m_ChosenFood [2]));
		SetBottom (ListOfIngredients(m_ChosenFood [3]));
		SetLeft (ListOfIngredients(m_ChosenFood [0]));
		SetRight (ListOfIngredients(m_ChosenFood [1]));

		string listOfFood = "";
		for (int i = 0; i < m_ChosenFood.Length; i++) {
			listOfFood += m_ChosenFood [i].name + ",";
		}
		SetListOfFood (listOfFood);
	}

	string ListOfIngredients(Food food) {
		string ingredients = "";
		for (int i = 0; i <= food.GetIngredientLevel() ;i++) {
			ingredients += food.GetIngredients () [i].Get_IngredientName() + "\n";
		}
		return ingredients;
	}


}
