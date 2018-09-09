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
        m_BackBun.SetActive(true);
    }

    protected override void SetupPosition()
    {
        base.SetupPosition();
        m_BackBun.transform.position += new Vector3(0, base.m_PlaceDownDistance, 0);
    }

    public override void SetAlpha(float percent)
    {
        base.SetAlpha(percent);
        m_BackBun.GetComponent<Image>().color = new Color(1,1,1,percent);
    }

    public override void StartAnimation()
    {
        Hashtable ht = new Hashtable();
        ht.Add("y", -base.m_PlaceDownDistance);
        ht.Add("easeType", "easeInOutExpo");
        ht.Add("time", base.animation.PlacementTime()+0.15);
        ht.Add("oncomplete", "ResizeAnimation");
        iTween.MoveAdd(m_BackBun, ht);
        base.StartAnimation();
    }

    protected override void ResizeAnimation(){
        base.ResizeAnimation();
        Hashtable ht = new Hashtable();
        ht.Add("scale", new Vector3(1.2f, 1.2f, 0));
        ht.Add("time", animation.PlacementTime());
        ht.Add("easeType", "spring");
        ht.Add("oncomplete", "WaitForPlaceDelegate");
        iTween.ScaleFrom(m_BackBun, ht);

    }

    protected override void WaitForPlaceDelegate()
    {
        base.WaitForPlaceDelegate();
        PlacementIngredientDelegate();
    }
}
