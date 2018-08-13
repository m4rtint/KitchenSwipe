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

    readonly protected float m_TimeTakenToPlace = 0.5f;
    readonly protected float m_PlaceDownDistance = 100;

    RectTransform m_RectTrans;
    #region Mono
    private void Awake()
    {
        m_RectTrans = GetComponent<RectTransform>();
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

    public virtual void SetCrossFadeAlpha(float duration)
    { 
        iTween.ColorTo(gameObject, Color.clear, duration);
    }
    #endregion

    #region Animation
    public virtual void StartAnimation()
    {
        Hashtable ht = new Hashtable();
        ht.Add("y",-m_PlaceDownDistance);
        ht.Add("easeType", "easeInOutExpo");
        ht.Add("time", m_TimeTakenToPlace);
        ht.Add("oncomplete", "WaitForPlaceDelegate");
        iTween.MoveAdd(gameObject, ht);
    }

    protected virtual void WaitForPlaceDelegate()
    {
        PlacementIngredientDelegate();
    }
    #endregion
}
