using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownGame : MonoBehaviour {

    private void Start()
    {
        resetText();
    }

    public void startCountDownAnimate()
    {
        changeText("READY?");
        Hashtable ht = new Hashtable();
        ht.Add("scale", new Vector3(2, 2, 0));
        ht.Add("time", 1.0f);
        ht.Add("oncomplete", "startGoAnimate");
        iTween.ScaleTo(gameObject, ht);
    }

    void startGoAnimate()
    {
        resetText();
        changeText("GO");
        Hashtable ht = new Hashtable();
        ht.Add("scale", new Vector3(2, 2, 0));
        ht.Add("time", 1.0f);
        ht.Add("oncomplete", "fadeOutObject");
        iTween.ScaleTo(gameObject, ht);
    }

    void fadeOutObject()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("time", 0.5f);
        ht.Add("oncomplete", "changeStateStartGame");
        iTween.FadeTo(gameObject, ht);
    }

    void changeStateStartGame()
    {
        StateManager.instance.startGame();
        Destroy(gameObject);
    }

    #region helper
    void resetText()
    {
        changeText();
        transform.localScale = Vector3.zero;
    }

    void changeText(string text = "")
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }
    #endregion
}
