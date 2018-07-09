using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ingredient:MonoBehaviour {
	[SerializeField]
	string m_IngredientName;
    [SerializeField]
    Sprite m_IngredientCenterImage;

	public string Get_IngredientName() {
		return m_IngredientName;
	}

    public Sprite CenterSpriteImage(){
        return m_IngredientCenterImage;
    }
}
