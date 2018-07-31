using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodAnimation : MonoBehaviour {

    readonly float m_FinishAnimationSpeed = 50;
    readonly Vector3 m_RiseUp = new Vector3(0, 50, 0);
    bool m_FinishFoodAnimation = false;
    Ingredient[] m_Ingredients;

    Vector3 m_EndPosition;
    RectTransform m_RectTrans;

    #region mono
    void Awake()
    {
        m_RectTrans = GetComponent<RectTransform>();
        m_Ingredients = GetComponent<Food>().GetIngredients();
    }

    void Start()
    {
        m_EndPosition = m_RectTrans.transform.localPosition + m_RiseUp;
    }

    // Update is called once per frame
    void Update () {
		if (m_FinishFoodAnimation)
        {
            Vector3 currentPosition = m_RectTrans.transform.localPosition;
            m_RectTrans.transform.localPosition = Vector3.MoveTowards(currentPosition, m_EndPosition, m_FinishAnimationSpeed * Time.deltaTime);
            if (m_RectTrans.transform.localPosition == m_EndPosition)
            {
                Destroy(gameObject);
            }
        }
	}
    #endregion

    #region Public
    public void StartFinishFoodAnimation()
    {
        m_FinishFoodAnimation = true;
        SetAlpha();
    }
    #endregion

    #region Colour
    void SetAlpha()
    {
        foreach(Ingredient ingredient in m_Ingredients)
        {
            ingredient.SetCrossFadeAlpha(0, 1f, false);
        }
    }
    #endregion
}
