using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour {
    //Dependencies
    FirebaseAuthentication auth;

    [Header("Login")]
    [SerializeField]
    GameObject m_LoginEmailObj;
    InputField m_LoginEmail;
    [SerializeField]
    GameObject m_LoginPassObj;
    InputField m_LoginPass;

    [Header("Registration")]
    [SerializeField]
    GameObject m_RegiEmailObj;
    InputField m_RegiEmail;
    [SerializeField]
    GameObject m_RegiPassObj;
    InputField m_RegiPass;
    [SerializeField]
    GameObject m_RegiDisplayObj;
    InputField m_RegiDisplay;

    [Header("Error")]
    [SerializeField]
    GameObject m_ErrorObj;

    #region mono
    private void Awake()
    {
        SetupInputFields();
		SetupDependencies();
		SetupDelegate();
    }

    void SetupInputFields()
    {
        m_LoginEmail = m_LoginEmailObj.GetComponent<InputField>();
        m_LoginPass = m_LoginPassObj.GetComponent<InputField>();
        m_RegiEmail =  m_RegiEmailObj.GetComponent<InputField>();
        m_RegiPass = m_RegiPassObj.GetComponent<InputField>();
        m_RegiDisplay = m_RegiDisplayObj.GetComponent<InputField>();

    }

    void SetupDependencies()
    {
		if (FirebaseAuthentication.instance == null) {
			OnError ("Auth is null");
		}
        auth = FirebaseAuthentication.instance;
    }

    void SetupDelegate()
    {
        auth.AuthDelegate += OnAuthenticate;
        auth.errorDelegate += OnError;
    }

    #endregion

    #region UserInteraction
    public void AnonSignIn()
    {
        string name = m_LoginEmail.text;
        FirebaseAuthentication.instance.AnonAuthentication(name);
    }

    public void EmailSignIn()
    {
        Dictionary<string, string> profile = new Dictionary<string, string>();
        profile["email"] = m_LoginEmail.text;
        profile["password"] = m_LoginPass.text;
        FirebaseAuthentication.instance.EmailAuthentication(profile);
    }

    public void EmailRegistration()
    {
        Dictionary<string, string> profile = new Dictionary<string, string>();
        profile["email"] = m_RegiEmail.text;
        profile["password"] = m_RegiPass.text;
        profile["displayname"] = m_RegiDisplay.text;
        FirebaseAuthentication.instance.EmailRegistration(profile);
    }

    #endregion

    #region DelegateFunctions
    void OnAuthenticate()
    {
        gameObject.SetActive(false);
    }

    void OnError(string description)
    {
        m_ErrorObj.GetComponent<ErrorManager>().SetErrorText(description);
        m_ErrorObj.SetActive(true);
    }

    #endregion

}
