using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBun : Ingredient {

    //BackBun Properties
    [SerializeField]
    GameObject m_BackBun;
    Vector3 m_BackbunEndPosition;
    RectTransform m_BackBunTrans;

    bool m_BackbunPlacementAnimation;

    private void OnEnable()
    {
        m_BackBun.SetActive(true);
        m_BackBunTrans = m_BackBun.GetComponent<RectTransform>();
        m_BackbunPlacementAnimation = false;
        m_BackbunEndPosition = m_BackBunTrans.localPosition - new Vector3(0, 100);
    }

    public override void SetAlpha(float percent)
    {
        base.SetAlpha(percent);
        m_BackBun.GetComponent<Image>().color = new Color(1,1,1,percent);
    }

    protected override void FoodPlacementAnimation()
    {
        base.FoodPlacementAnimation();
        if (m_BackbunPlacementAnimation)
        {
            Vector3 currentPosition = m_BackBunTrans.transform.localPosition;
            m_BackBunTrans.transform.localPosition = Vector3.MoveTowards(currentPosition, m_BackbunEndPosition, m_PlacementAnimationSpeed * Time.deltaTime);
            if (m_BackBunTrans.transform.localPosition == m_BackbunEndPosition)
            {
                m_BackbunPlacementAnimation = false;
            }
        }
    }

    public override void StartAnimation()
    {
        base.StartAnimation();
        m_BackbunPlacementAnimation = true;
    }

    public override void SetCrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
    {
        base.SetCrossFadeAlpha(alpha, duration, ignoreTimeScale);
        m_BackBun.GetComponent<Image>().CrossFadeAlpha(alpha, duration, ignoreTimeScale);
    }
}
