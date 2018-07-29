using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBun : Ingredient {

    [SerializeField]
    GameObject m_BackBun;

    private void OnEnable()
    {
        m_BackBun.SetActive(true);
    }

    public override void SetAlpha(float percent)
    {
        base.SetAlpha(percent);
        m_BackBun.GetComponent<Image>().color = new Color(1,1,1,percent);
    }

}
