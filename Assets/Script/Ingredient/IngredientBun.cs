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
    

    private void OnEnable()
    {
        m_BackBun.SetActive(true);
        m_BackBunTrans = m_BackBun.GetComponent<RectTransform>();
        m_BackbunEndPosition = m_BackBunTrans.localPosition - new Vector3(0, 100);
    }

    public override void SetAlpha(float percent)
    {
        base.SetAlpha(percent);
        m_BackBun.GetComponent<Image>().color = new Color(1,1,1,percent);
    }

    public override void StartAnimation()
    {
        base.StartAnimation();
        iTween.MoveBy(m_BackBun, iTween.Hash("x", base.m_EndPosition.x, "y", base.m_EndPosition.y, "easeType", "easeInOutExpo", "time", base.m_TimeTakenToPlace));
        StartCoroutine("WaitForPlaceDelegate");
    }

    protected override IEnumerator WaitForPlaceDelegate()
    {
        base.WaitForPlaceDelegate();
        yield return new WaitForSeconds(base.m_TimeTakenToPlace);
        PlacementIngredientDelegate();
    }


    public override void SetCrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
    {
        base.SetCrossFadeAlpha(alpha, duration, ignoreTimeScale);
        m_BackBun.GetComponent<Image>().CrossFadeAlpha(alpha, duration, ignoreTimeScale);
    }
}
