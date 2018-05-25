using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOfFoodManager : MonoBehaviour
{

	[SerializeField]
	GameObject List_1;
	[SerializeField]
	GameObject List_2;
	[SerializeField]
	GameObject List_3;
	[SerializeField]
	GameObject List_4;
	[SerializeField]
	GameObject List_5;

	void Awake()
	{
		GameObject[] m_AllLists = new GameObject[]{List1, List2,List3,List4,List5
};
	}
	public void PlaceFoodOntoList() {

	}


}
