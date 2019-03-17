using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnouncementManager : MonoBehaviour {

    public delegate void AnnouncementDelegate();
    public AnnouncementDelegate onTimesUpComplete;

    private void Start()
    {
        resetText();
    }

    #region Public
    public void startTimesUpAnimate()
    {
        resetText("TIME'S\nUP!");
        Hashtable ht = new Hashtable();
        ht.Add("scale", new Vector3(2, 2, 0));
        ht.Add("time", 1.5f);
        ht.Add("easetype", "easeOutExpo");
        ht.Add("oncomplete", "onEndTimesUpAnimate");
        iTween.ScaleTo(gameObject, ht);
    }
    #endregion

    void onEndTimesUpAnimate()
    {
        onTimesUpComplete();
        gameObject.SetActive(false);
    }

    void fadeOutObject()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("time", 0.05f);
        ht.Add("oncomplete", "changeStateStartGame");
        iTween.FadeTo(gameObject, ht);
    }

    void changeStateStartGame()
    {
        StateManager.instance.startGame();
        gameObject.SetActive(false);
    }

    #region helper
    void resetText(string text = "")
    {
        changeText(text);
        transform.localScale = Vector3.zero;
        GetComponent<TextMeshProUGUI>().color = Color.black;
    }

    void changeText(string text = "")
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }
    #endregion
}
