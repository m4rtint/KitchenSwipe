using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class NextIngredient : MonoBehaviour {
    
    Ingredient ingredient;

    readonly Vector2 minLargeIngredientSize = new Vector2(0, 0.285f);
    readonly Vector2 maxLargeIngredientSize = new Vector2(1, 0.715f);


    public void Ingredient(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        if (ingredient != null) { 
            GetComponent<Image>().sprite = ingredient.CenterSpriteImage();
            setSizeIfNeeded();
        } else
        {
            GetComponent<Image>().color = Color.clear;
        }
    }

    public Ingredient Ingredient()
    {
        return this.ingredient;
    }
    
    void setSizeIfNeeded()
    {
        if (ingredient.isUpNextSizeChangeNeeded)
        {
            GetComponent<RectTransform>().anchorMin = minLargeIngredientSize;
            GetComponent<RectTransform>().anchorMax = maxLargeIngredientSize;
        } else
        {
            GetComponent<RectTransform>().anchorMin = Vector2.zero;
            GetComponent<RectTransform>().anchorMax = Vector2.one;
        }
    }
	
}
