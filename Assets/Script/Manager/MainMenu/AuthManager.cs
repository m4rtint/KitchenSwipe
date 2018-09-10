using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour {
    //Dependencies
    FirebaseAuthentication auth;

    [Header("Login")]
    [SerializeField]
    GameObject loginEmailFieldObj;
    InputField loginEmail;
    [SerializeField]
    GameObject loginPasswordFieldObj;
    InputField loginPass;

    [Header("Registration")]
    [SerializeField]
    GameObject registerEmailFieldObj;
    InputField registerEmail;
    [SerializeField]
    GameObject registerPasswordFieldObj;
    InputField registerPass;
    [SerializeField]
    GameObject registerDisplayNameFieldObj;
    InputField registerDisplayName;

    [Header("Loading")]
    [SerializeField]
    GameObject loadingObject;

    [Header("Error")]
    [SerializeField]
    GameObject errorObj;

    #region mono
    private void Awake()
    {
        setupInputFields();
		setupDependencies();
		setupDelegate();
    }

    void setupInputFields()
    {
        loginEmail = loginEmailFieldObj.GetComponent<InputField>();
        loginPass = loginPasswordFieldObj.GetComponent<InputField>();
        registerEmail =  registerEmailFieldObj.GetComponent<InputField>();
        registerPass = registerPasswordFieldObj.GetComponent<InputField>();
        registerDisplayName = registerDisplayNameFieldObj.GetComponent<InputField>();

    }

    void setupDependencies()
    {
		if (FirebaseAuthentication.instance == null) {
			onError ("Auth is null");
		}
        auth = FirebaseAuthentication.instance;
    }

    void setupDelegate()
    {
        auth.authDelegate += onAuthenticate;
        auth.errorDelegate += onError;
    }

    #endregion

    #region UserInteraction
    public void anonSignIn()
    {
        string anonName = loginEmail.text;
        FirebaseAuthentication.instance.anonAuthentication(anonName);
    }

    public void emailSignIn()
    {
        Dictionary<string, string> profile = new Dictionary<string, string>();
        profile["email"] = loginEmail.text;
        profile["password"] = loginPass.text;
        FirebaseAuthentication.instance.emailAuthentication(profile);
    }

    public void emailRegistration()
    {
        Dictionary<string, string> profile = new Dictionary<string, string>();
        profile["email"] = registerEmail.text;
        profile["password"] = registerPass.text;
        profile["displayname"] = registerDisplayName.text;
        FirebaseAuthentication.instance.emailRegistration(profile);
    }

    #endregion

    #region DelegateFunctions
    void onAuthenticate()
    {
        gameObject.SetActive(false);
    }

    void onError(string description)
    {
        errorObj.GetComponent<ErrorManager>().setErrorText(description);
        errorObj.SetActive(true);
    }

    #endregion

    #region helper
    void animateLoading()
    {
        Hashtable ht = new Hashtable();
        ht.Add("z", 1);
        ht.Add("easeType", "easeInOutBack");
        ht.Add("looptype", "loop");
        iTween.RotateBy(loadingObject, ht);
    }

    void startLoading()
    {
        loadingObject.SetActive(true);
        animateLoading();
    }
    #endregion
}
