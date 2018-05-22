using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient:MonoBehaviour {
	[SerializeField]
	string m_IngredientName;

	public string Get_IngredientName() {
		return m_IngredientName;
	}
}
