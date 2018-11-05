using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class NextIngredient : MonoBehaviour {
    
    Ingredient ingredient;
    
    public void Ingredient(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        if (ingredient != null) { 
            GetComponent<Image>().sprite = ingredient.CenterSpriteImage();
        } else
        {
            GetComponent<Image>().sprite = null;
        }
    }

    public Ingredient Ingredient()
    {
        return this.ingredient;
    }
    
	
}
