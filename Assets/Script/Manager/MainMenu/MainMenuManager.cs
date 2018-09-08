using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject ErrorObject;
	[SerializeField]
	GameObject authenticationObject;

    #region Mono
    private void Awake()
    {
        setupDelegate();
    }

    void setupDelegate()
    {
		FirebaseAuthentication.instance.signOutDelegate += displayAuth;
        FirebaseDB.instance.errorDelegate += displayError;
    }
    #endregion

	#region Public
	public void LogOut(){
		FirebaseAuthentication.instance.logOut ();
	}
	#endregion

    #region Delegate
	void displayAuth()
	{
		authenticationObject.SetActive (true);
	}

    void displayError(string message)
    {
        ErrorObject.SetActive(true);
        ErrorObject.GetComponent<ErrorManager>().setErrorText(message);
    }
    #endregion
}
