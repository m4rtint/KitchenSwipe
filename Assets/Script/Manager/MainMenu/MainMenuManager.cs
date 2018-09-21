using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject ErrorObject;
	[SerializeField]
	GameObject authenticationObject;
    [SerializeField]
    GameObject displayname;

    [SerializeField]
    GameObject[] loadings;

    #region Mono
    private void Awake()
    {
        setupDelegate();
        animateLoading();
    }

    void setupDelegate()
    {
        FirebaseAuthentication.instance.profileUpdateDelegate += displayUserName;
		FirebaseAuthentication.instance.signOutDelegate += displayAuth;
        FirebaseDB.instance.errorDelegate += displayError;
    }

    void animateLoading()
    {
        Hashtable ht = new Hashtable();
        ht.Add("z", 1);
        ht.Add("easeType", "easeInOutBack");
        ht.Add("looptype", "loop");
        foreach (GameObject obj in loadings)
        {
            iTween.RotateBy(obj, ht);
        }
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

    void displayUserName()
    {
        displayname.GetComponent<TextMeshProUGUI>().text = FirebaseAuthentication.instance.displayName();
    }

    #endregion
}
