using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject displayNameObject;
	[SerializeField]
	GameObject authenticationObject;

    #region Mono
    private void Awake()
    {
        setupDelegate();
    }

    void setupDelegate()
    {
        FirebaseAuthentication.instance.profileUpdateDelegate += updateDisplayName;
		FirebaseAuthentication.instance.signOutDelegate += displayAuth;
    }
    #endregion

	#region Public
	public void LogOut(){
		FirebaseAuthentication.instance.logOut ();
	}
	#endregion

    #region Delegate
    void updateDisplayName()
    {
        displayNameObject.GetComponent<Text>().text = FirebaseAuthentication.instance.displayName();
    }

	void displayAuth()
	{
		authenticationObject.SetActive (true);
	}
    #endregion
}
