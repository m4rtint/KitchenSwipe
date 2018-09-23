using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour {

    [SerializeField]
    Text comboText;

    public void setComboText(string text)
    {
        comboText.text = text;
    }

    public GameObject getTextObject()
    {
        return comboText.gameObject;
    }
}
