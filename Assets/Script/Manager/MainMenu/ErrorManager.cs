using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorManager : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI errorTextObj;


    public void setErrorText(string text)
    {
        errorTextObj.text = text;
    }
}
