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

    [Header("Buttons Animation")]
    [SerializeField]
    GameObject[] buttons;

    #region Mono
    private void Awake()
    {
        setupDelegate();
        animateLoading();
        resetButtons();
        resetDisplayUserName();
    }

    void setupDelegate()
    {
        FirebaseAuthentication.instance.authDelegate += animateButtons;
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

    void resetButtons()
    {
        foreach(GameObject obj in buttons)
        {
            obj.transform.localScale = Vector3.zero;
        }
    }

    void resetDisplayUserName()
    {
        displayname.GetComponent<TextMeshProUGUI>().text = "";
    }
    #endregion

    #region Public
    public void LogOut(){
        resetButtons();
        resetDisplayUserName();
        FirebaseAuthentication.instance.logOut ();
	}
	#endregion

    #region Delegate
    void animateButtons()
    {
        foreach(GameObject obj in buttons) {
            Hashtable ht = new Hashtable();
            ht.Add("scale", Vector3.one);
            ht.Add("easeType", "easeOutBack");
            ht.Add("time", 0.5f);
            iTween.ScaleTo(obj, ht);
        }
    }

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
