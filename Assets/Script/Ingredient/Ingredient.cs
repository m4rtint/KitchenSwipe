using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Ingredient:MonoBehaviour {
    //Delegate
    public delegate void IngredientAnimationDelegate();
    public IngredientAnimationDelegate PlacementIngredientDelegate;

	[SerializeField]
	string m_IngredientName;

    [SerializeField]
    Sprite m_CenterImage;

    protected float m_TimeTakenToPlace = 0.5f;
    
    protected Vector3 m_EndPosition;

    RectTransform m_RectTrans;
    #region Mono
    private void Awake()
    {
        m_RectTrans = GetComponent<RectTransform>();
        m_EndPosition = m_RectTrans.localPosition - new Vector3(0, 100);
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
        iTween.MoveBy(gameObject, iTween.Hash("x", m_EndPosition.x, "y", m_EndPosition.y, "easeType", "easeInOutExpo", "time", m_TimeTakenToPlace));
        StartCoroutine("WaitForPlaceDelegate");
    }

    protected virtual IEnumerator WaitForPlaceDelegate()
    {
        yield return new WaitForSeconds(m_TimeTakenToPlace);
        PlacementIngredientDelegate();
    }
    #endregion
}
