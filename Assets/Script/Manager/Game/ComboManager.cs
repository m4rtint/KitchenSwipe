using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour {

    [SerializeField]
    Text comboText;

    GameObject comboGameObject()
    {
        return comboText.gameObject;
    }

    public void setComboText(int combo)
    {
        comboText.text = "x"+combo;
    }

    public GameObject getTextObject()
    {
        return comboText.gameObject;
    }

    public void animateCombo(int combo)
    {
        setComboText(combo);
        comboGameObject().transform.localScale = Vector3.one;
        Hashtable ht = new Hashtable();
        ht.Add("scale", AnimationManager.instance.ComboPopScale());
        ht.Add("time", AnimationManager.instance.ComboPopTime());
        ht.Add("easeType", "easeInQuart");
        iTween.ScaleFrom(comboGameObject(), ht);
    }
}
