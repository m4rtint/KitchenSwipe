using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour {

    [SerializeField]
    GameObject m_ErrorTextObj;


    public void SetErrorText(string text)
    {
        m_ErrorTextObj.GetComponent<Text>().text = text;
    }
}
