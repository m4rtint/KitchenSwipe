using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Ingredient:MonoBehaviour {
    //Delegate
    public delegate void IngredientAnimationDelegate();
    public IngredientAnimationDelegate placementIngredientDelegate;

	[SerializeField]
	string m_IngredientName;

    [SerializeField]
    Sprite m_CenterImage;

    [SerializeField]
    Sprite outlineSprite;

    [SerializeField]
    float customPlaceDownDistance;

    protected float placeDownDistance = 100;
    protected AnimationManager animation;

    #region Mono
    private void Awake()
    {
        animation = AnimationManager.instance;
        setupPosition();
    }

    protected virtual void setupPosition()
    {
        if (customPlaceDownDistance != 0)
        {
            placeDownDistance = customPlaceDownDistance;
        }
        transform.position += new Vector3(0, placeDownDistance, 0);
    }
    #endregion

    #region GetterSetter
    public string IngredientName() {
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

    public void setOutlineImage(bool outline)
    {
        Sprite img = outline ? outlineSprite : null;
        GetComponent<Image>().overrideSprite = img;
    }

    public virtual void setAlpha(float percent)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, percent);
    }
    #endregion

    #region Animation
    public virtual void startAnimation()
    {
        Hashtable ht = new Hashtable();
        ht.Add("y",-placeDownDistance);
        ht.Add("easeType", "easeInQuint");
        ht.Add("time", animation.PlacementTime());
        ht.Add("oncomplete", "resizeAnimation");
        iTween.MoveAdd(gameObject, ht);
    }

    protected virtual void resizeAnimation()
    {
        Hashtable ht = new Hashtable();
        ht.Add("scale", new Vector3(1.2f,1.2f,0));
        ht.Add("time", animation.PlacementTime());
        ht.Add("easeType", "spring");
        ht.Add("oncomplete", "waitForPlaceDelegate");
        iTween.ScaleFrom(gameObject, ht);
    }

    public void pulsingAnimation() {
        IngredientEffects effects = GetComponent<IngredientEffects>();
        if (effects != null) {
            effects.pulsingAnimation();
        }
    }

    public void resetScale() {
        IngredientEffects effects = GetComponent<IngredientEffects>();
        if (effects != null) {
            effects.resetScale();
        }
    }

    protected virtual void waitForPlaceDelegate()
    {
        placementIngredientDelegate();
    }
    #endregion
}
