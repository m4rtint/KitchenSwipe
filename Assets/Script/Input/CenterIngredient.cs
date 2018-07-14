using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CenterIngredient : MonoBehaviour {

	public void SetCenter(Ingredient ingredient)
    {
        GetComponent<Image>().sprite = ingredient.CenterSpriteImage();
    }
}
