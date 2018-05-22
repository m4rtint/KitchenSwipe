using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
public class GameEngine : MonoBehaviour {

	FoodGenerator m_FoodGenerator;

	// Use this for initialization
	void Awake () {
		m_FoodGenerator = GetComponent<FoodGenerator>();
	}

	private void Start()
	{
		m_FoodGenerator.ChooseFourRandomFood();
		m_FoodGenerator.DebugPrintEachSide();
	}

}
