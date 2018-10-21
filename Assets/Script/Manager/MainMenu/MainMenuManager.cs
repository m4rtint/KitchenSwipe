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
    GameObject leaderboard;
    [SerializeField]
    GameObject signOut;
    [SerializeField]
    GameObject background;
    [SerializeField]
    GameObject[] loadings;

    [Header("UI")]
    [SerializeField]
    TextMeshProUGUI displayname;


    [Header("Buttons")]
    [SerializeField]
    GameObject[] buttons;
    [SerializeField]
    Button leaderboardButton;
    [SerializeField]
    Button signOutButton;

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
        leaderboardButton.onClick.AddListener(delegate
        {
            hideButtons();
            leaderboard.SetActive(true);
        });

        signOutButton.onClick.AddListener(delegate
        {
            hideButtons();
            iTween.ScaleTo(signOut, Vector3.one, 0.5f);
        });
    }

    void setupDelegate()
    {
        FirebaseAuthentication.instance.authDelegate += showButtons;
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
        displayname.text = "";
    }

    void hideButtons()
    {
        background.SetActive(true);
        animateButtons(false);
    }

    void animateButtons(bool open)
    {
        Vector3 size = open ? Vector3.one : Vector3.zero;
        foreach (GameObject obj in buttons)
        {
            Hashtable ht = new Hashtable();
            ht.Add("scale", size);
            ht.Add("easeType", "easeOutBack");
            ht.Add("time", 0.5f);
            iTween.ScaleTo(obj, ht);
        }
    }
    #endregion

    #region Public
    public void LogOut(){
        resetButtons();
        resetDisplayUserName();
        FirebaseAuthentication.instance.logOut();
        signOut.SetActive(false);
        background.SetActive(false);
	}

    public void closePanels()
    {
        if (leaderboard.activeSelf)
        {
            leaderboard.SetActive(false);
        }
        
        if (signOut.activeSelf)
        {
            signOut.SetActive(false);
        }

        background.SetActive(false);
        showButtons();
    }

	#endregion

    #region Delegate
    void showButtons()
    {
        animateButtons(true);
    }

    void displayError(string message)
    {
        ErrorObject.SetActive(true);
        ErrorObject.GetComponent<ErrorManager>().setErrorText(message);
    }

    void displayUserName()
    {
        displayname.text = FirebaseAuthentication.instance.displayName();
    }

    #endregion
}
