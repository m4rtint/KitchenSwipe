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

    bool m_PlacementAnimation;
    protected readonly float m_PlacementAnimationSpeed = 2000;
    Vector3 m_EndPosition;

    RectTransform m_RectTrans;
    #region Mono
    private void Awake()
    {
        m_RectTrans = GetComponent<RectTransform>();
        m_PlacementAnimation = false;
        m_EndPosition = m_RectTrans.localPosition - new Vector3(0, 100);
    }

    private void Update()
    {
        FoodPlacementAnimation();
    }
    #endregion

    #region GetterSetter
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

    public virtual void SetAlpha(float percent)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, percent);
    }

    public virtual void SetCrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
    {
        GetComponent<Image>().CrossFadeAlpha(alpha, duration, ignoreTimeScale);
    }
    #endregion

    #region Animation
    public virtual void StartAnimation()
    {
        m_PlacementAnimation = true;
    }

    protected virtual void FoodPlacementAnimation()
    {
        if (m_PlacementAnimation)
        {
            Vector3 currentPosition = m_RectTrans.transform.localPosition;
            m_RectTrans.transform.localPosition = Vector3.MoveTowards(currentPosition, m_EndPosition, m_PlacementAnimationSpeed * Time.deltaTime);
            if (m_RectTrans.transform.localPosition == m_EndPosition)
            {
                m_PlacementAnimation = false;
            }
        }
    }
    #endregion
}
