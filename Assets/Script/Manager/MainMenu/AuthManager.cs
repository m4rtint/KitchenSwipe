using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour {

    [SerializeField]
    GameObject m_NameFieldObject;
    InputField m_NameInputField;

    #region mono
    private void Awake()
    {
        m_NameInputField = m_NameFieldObject.GetComponent<InputField>();
        SetupDelegate();
        SetupRandomUserName();
    }

    void SetupDelegate()
    {
        FirebaseAuthentication.instance.anonAuthDelegate += OnAuthenticate;
    }

    void SetupRandomUserName()
    {
        int number = Random.Range(100, 1000);
        m_NameInputField.text = "Guest " + number;
    }
    #endregion

    #region UserInteraction
    public void AnonSignIn()
    {
        string name = m_NameInputField.text;
        FirebaseAuthentication.instance.AnonAuthentication(name);
    }

    #endregion

    #region DelegateFunctions
    void OnAuthenticate()
    {
        gameObject.SetActive(false);
    }

    #endregion

}
