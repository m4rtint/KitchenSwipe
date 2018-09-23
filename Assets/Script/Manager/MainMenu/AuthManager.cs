using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour {
    //Dependencies
    FirebaseAuthentication auth;

    [Header("Login")]
    [SerializeField]
    EmailSignInManager emailSignInManager;

    [Header("Registration")]
    [SerializeField]
    EmailRegisterManager emailRegistrationManager;

    [Header("Loading")]
    [SerializeField]
    GameObject loadingObject;

    [Header("Error")]
    [SerializeField]
    GameObject errorObj;

    #region mono
    private void Awake()
    {
        setupAuthenticationPanel();
        setupDependencies();
		setupDelegate();
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
        auth.signOutDelegate += onSignOut;
        auth.authDelegate += onAuthenticate;
        auth.errorDelegate += onError;
    }

    void setupAuthenticationPanel()
    {
        emailSignInManager.gameObject.SetActive(false);
        emailRegistrationManager.gameObject.SetActive(false);
    }

    #endregion

    #region UserInteraction
    public void anonSignIn()
    {
        loadingObject.SetActive(true);
        FirebaseAuthentication.instance.anonAuthentication();
    }

    public void emailSignIn()
    {
        emailSignInManager.emailSignIn();
        loadingObject.SetActive(true);
    }

    public void emailRegistration()
    {
        emailRegistrationManager.emailRegistration();
        loadingObject.SetActive(true);
    }

    #endregion

    #region DelegateFunctions
    void onAuthenticate()
    {
        loadingObject.SetActive(false);
        gameObject.SetActive(false);
    }

    void onError(string description)
    {
        errorObj.GetComponent<ErrorManager>().setErrorText(description);
        errorObj.SetActive(true);
        loadingObject.SetActive(false);
    }

    void onSignOut()
    {
        setupAuthenticationPanel();
        gameObject.SetActive(true);
    }

    #endregion
}
