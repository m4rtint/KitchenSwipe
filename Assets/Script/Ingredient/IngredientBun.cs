using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBun : Ingredient {

    //BackBun Properties
    [SerializeField]
    GameObject m_BackBun;
    Vector3 m_BackbunEndPosition;
    

    private void OnEnable()
    {
        m_BackBun.SetActive(false);
    }

    protected override void setupPosition()
    {
        base.setupPosition();
        m_BackBun.transform.position += new Vector3(0, base.placeDownDistance, 0);
    }

    public override void setAlpha(float percent)
    {
        base.setAlpha(percent);
        m_BackBun.GetComponent<Image>().color = new Color(1,1,1,percent);
    }

    public override void startAnimation()
    {
        m_BackBun.SetActive(true);
        Hashtable ht = new Hashtable();
        ht.Add("y", -base.placeDownDistance);
        ht.Add("easeType", "easeInOutExpo");
        ht.Add("time", base.animation.PlacementTime());
        ht.Add("oncomplete", "resizeAnimation");
        iTween.MoveAdd(m_BackBun, ht);
        base.startAnimation();
    }

    protected override void resizeAnimation(){
        base.resizeAnimation();
        Hashtable ht = new Hashtable();
        ht.Add("scale", new Vector3(1.2f, 1.2f, 0));
        ht.Add("time", animation.PlacementTime());
        ht.Add("easeType", "spring");
        ht.Add("oncomplete", "waitForPlaceDelegate");
        iTween.ScaleFrom(m_BackBun, ht);

    }

    protected override void waitForPlaceDelegate()
    {
        base.waitForPlaceDelegate();
        placementIngredientDelegate();
    }
}
