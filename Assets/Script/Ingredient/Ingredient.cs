using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Ingredient:MonoBehaviour {
	[SerializeField]
	string m_IngredientName;

    [SerializeField]
    Sprite m_CenterImage;

    public string Get_IngredientName() {
		return m_IngredientName;
	}

    public Sprite CenterSpriteImage(){
        if (m_CenterImage != null)
        {
            return m_CenterImage;
        } else { 
            return GetComponent<Image>().sprite;
        }
    }

    public virtual void SetSolidAlpha()
    {
        GetComponent<Image>().color = Color.white;
    }
}
