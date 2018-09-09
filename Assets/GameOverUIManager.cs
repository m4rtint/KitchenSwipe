using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour {

    private void OnEnable()
    {
        startFadeIn();
    }

    void startFadeIn()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("time", AnimationManager.instance.GameOverFadeInTime());
        iTween.FadeFrom(gameObject, ht);
    }
}
