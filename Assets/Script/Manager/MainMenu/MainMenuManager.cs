using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    GameObject ErrorObject;
	[SerializeField]
	GameObject authenticationObject;
    [SerializeField]
    GameObject leaderboard;
    bool isLeaderboardActive;
    [SerializeField]
    GameObject[] loadings;

    [Header("UI")]
    [SerializeField]
    GameObject displayname;


    [Header("Buttons")]
    [SerializeField]
    GameObject[] buttons;
    [SerializeField]
    Button leaderboardButton;

    #region Mono
    private void Awake()
    {
        setupDelegate();
        animateLoading();
        setupButtons();
        resetDisplayUserName();
    }

    void setupButtons() {
        resetButtons();
        isLeaderboardActive = false;
        leaderboardButton.onClick.AddListener(delegate {
            isLeaderboardActive = !isLeaderboardActive;
            leaderboard.SetActive(isLeaderboardActive);
        });
    }

    void setupDelegate()
    {
        FirebaseAuthentication.instance.authDelegate += animateButtons;
        FirebaseAuthentication.instance.profileUpdateDelegate += displayUserName;
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
