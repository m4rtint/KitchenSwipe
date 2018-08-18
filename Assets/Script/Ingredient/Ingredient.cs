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

    readonly protected float m_PlaceDownDistance = 100;
    protected AnimationManager animation;

    #region Mono
    private void Awake()
    {
        animation = AnimationManager.instance;
        SetupPosition();
    }

    protected virtual void SetupPosition()
    {
        transform.position += new Vector3(0, m_PlaceDownDistance, 0);
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
        Color white = new Color(1, 1, 1, 0);
        iTween.ColorTo(gameObject, white, duration);
    }
    #endregion

    #region Animation
    public virtual void StartAnimation()
    {
        Hashtable ht = new Hashtable();
        ht.Add("y",-m_PlaceDownDistance);
        ht.Add("easeType", "easeInQuint");
        ht.Add("time", animation.PlacementTime());
        ht.Add("oncomplete", "ResizeAnimation");
        iTween.MoveAdd(gameObject, ht);
    }

    protected virtual void ResizeAnimation()
    {
        Hashtable ht = new Hashtable();
        ht.Add("scale", new Vector3(1.2f,1.2f,0));
        ht.Add("time", animation.PlacementTime());
        ht.Add("easeType", "spring");
        ht.Add("oncomplete", "WaitForPlaceDelegate");
        iTween.ScaleFrom(gameObject, ht);
    }



    protected virtual void WaitForPlaceDelegate()
    {
        PlacementIngredientDelegate();
    }
    #endregion
}
