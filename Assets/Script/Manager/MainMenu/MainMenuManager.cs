using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject DisplayNameObject;
	[SerializeField]
	GameObject AuthenticationObject;

    #region Mono
    private void Awake()
    {
        SetupDelegate();
    }

    void SetupDelegate()
    {
        FirebaseAuthentication.instance.profileUpdateDelegate += UpdateDisplayName;
		FirebaseAuthentication.instance.SignOutDelegate += DisplayAuth;
    }
    #endregion

	#region Public
	public void LogOut(){
		FirebaseAuthentication.instance.LogOut ();
	}
	#endregion

    #region Delegate
    void UpdateDisplayName()
    {
        DisplayNameObject.GetComponent<Text>().text = FirebaseAuthentication.instance.DisplayName();
    }

	void DisplayAuth()
	{
		AuthenticationObject.SetActive (true);
	}
    #endregion
}
