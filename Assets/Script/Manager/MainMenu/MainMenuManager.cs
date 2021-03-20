using System.Collections;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField]
    GameObject background;

    [Header("UI")]
    [SerializeField]
    TextMeshProUGUI displayname;
    
    [Header("Buttons")]
    [SerializeField]
    GameObject[] buttons;

    #region Mono
    private void Awake()
    {
        setupButtons();
        resetDisplayUserName();
    }

    void setupButtons() {
        resetButtons();
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
        background.SetActive(false);
	}

    public void closePanels()
    {
        background.SetActive(false);
        showButtons();
    }

	#endregion

    #region Delegate
    void showButtons()
    {
        animateButtons(true);
    }

    #endregion
}
