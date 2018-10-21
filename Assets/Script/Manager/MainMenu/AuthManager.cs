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

    [Header("Tutorial Instructions Panel")]
    [SerializeField]
    TutorialConfirmation tutorialConfirmation;


    bool isTutorialNeeded = false;
    #region mono
    private void Awake()
    {
        setupAuthenticationPanel();
        setupDependencies();
		setupDelegate();
        iTween.ScaleTo(gameObject, Vector3.one, 0.5f);
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
        isTutorialNeeded = true;
        loadingObject.SetActive(true);
        FirebaseAuthentication.instance.anonAuthentication();
    }

    public void emailSignIn()
    {
        isTutorialNeeded = false;
        emailSignInManager.emailSignIn();
        loadingObject.SetActive(true);
    }

    public void emailRegistration()
    {
        isTutorialNeeded = true;
        emailRegistrationManager.emailRegistration();
        loadingObject.SetActive(true);
    }

    #endregion

    #region DelegateFunctions
    void onAuthenticate()
    {
        if (isTutorialNeeded) {
            tutorialConfirmation.showTutorialConfirmation();
        }
        loadingObject.SetActive(false);
        gameObject.SetActive(false);

    }

    void onError(string description)
    {
        errorObj.SetActive(true);
        loadingObject.SetActive(false);
        errorObj.GetComponent<ErrorManager>().setErrorText(description);
    }

    void onSignOut()
    {
        setupAuthenticationPanel();
        gameObject.SetActive(true);
    }

    #endregion
}
