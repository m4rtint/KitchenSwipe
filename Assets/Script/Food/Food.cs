using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodAnimation))]
public class Food : MonoBehaviour {

	[SerializeField]
	Ingredient[] m_Ingredients;
    [SerializeField]
    FoodName name;
    float secondsToComplete;

	int level;
    FoodAnimation animation;

	#region Mono
	void Awake(){
		setupIngredientLevel ();
        setupIngredientColor();
        animation = GetComponent<FoodAnimation>();
	}


    void setupIngredientColor()
    {
        foreach(Ingredient ingredient in m_Ingredients)
        {
            float alpha = AnimationManager.instance.NeededIngredientAlpha();
            ingredient.setAlpha(alpha);
            ingredient.setOutlineImage(true);
            ingredient.gameObject.SetActive(false);
        }
        m_Ingredients[0].gameObject.SetActive(true);
    }
    #endregion

    #region Getter/Setter
    public FoodAnimation Animation() {
        return animation;
    }

    public int maxIngredientLevel()
    {
        return Ingredients().Length - 1;
    }

    public FoodName getName()
    {
        return name;
    }

    public Ingredient[] Ingredients() {
		return m_Ingredients;
	}

	public Ingredient GetNeededIngredient() {
        if (!isFoodInPlay()){ Debug.Log("Error - Tried to access index out of bounds Ingredient");}

        level = Mathf.Min (maxIngredientLevel(), level);
		return m_Ingredients [level];
	}

    public bool isFoodInPlay()
    {
        return level <= maxIngredientLevel();
    }

    void setupIngredientLevel() {
		level = 0;
        secondsToComplete = m_Ingredients.Length * 10f;
	}
    
	//For Order timing on top
	public float SecondsToComplete() {
        return secondsToComplete;
	}
	#endregion

	#region UserAction
	public void PlaceIngredient() {
        //Ingredient not active yet 
        if (!GetNeededIngredient().gameObject.activeSelf)
        {
            activeTopIngredientIfNeeded();
        }
        GetNeededIngredient().setAlpha(1);
        GetNeededIngredient().setOutlineImage(false);
        GetNeededIngredient().startAnimation();
        level++;
    }

    public void activeTopIngredientIfNeeded(){
        if (isFoodInPlay())
        {
            GetNeededIngredient().gameObject.SetActive(true);
        }
    }

   
	#endregion
}

