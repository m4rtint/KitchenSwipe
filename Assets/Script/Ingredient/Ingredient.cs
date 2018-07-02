using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ingredient:MonoBehaviour {
	[SerializeField]
	string m_IngredientName;

	public string Get_IngredientName() {
		return m_IngredientName;
	}

    public Color GetColor(){
        return GetComponent<Image>().color;
    }
}
