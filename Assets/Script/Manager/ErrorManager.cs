using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour {

    [SerializeField]
    GameObject errorTextObj;


    public void setErrorText(string text)
    {
        errorTextObj.GetComponent<Text>().text = text;
    }
}
