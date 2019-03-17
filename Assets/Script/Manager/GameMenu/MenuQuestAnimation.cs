using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuQuestAnimation : MonoBehaviour {

    public delegate void MenuQuestDelegate();
    public MenuQuestDelegate onAnimationCompleteDelegate;
    public MenuQuestDelegate onAnimationNewQuestDelegate;

    [SerializeField]
    TextMeshProUGUI questText;

    public int questId; 

    [SerializeField]
    GameObject checkMark;

    bool isAnimationComplete;

    private void Awake()
    {
        setCheckMarkAppear(false);
    }

    public void setQuestText(string text)
    {
        questText.text = text;
        checkMark.GetComponent<Image>().color = Color.white;
        fadeInOut(questText.gameObject, 1f, "onNewQuestPlaced");
    }

    public void questCompleteAnimation()
    {
        setCheckMarkAppear(true);
        scaleFrom(checkMark, "fadeOutCompletedQuest");
    }


    void scaleFrom(GameObject scaleObj, string onComplete = null)
    {
        Hashtable ht = new Hashtable();
        ht.Add("scale", Vector3.one * 3);
        ht.Add("easetype", "easeOutBack");
        ht.Add("time", 1f);
        if (onComplete != null)
        {
            ht.Add("oncompletetarget", gameObject);
            ht.Add("oncomplete", onComplete);
        }
        iTween.ScaleFrom(scaleObj, ht);
    }

    void fadeOutCompletedQuest()
    {
        fadeInOut(checkMark);
        fadeInOut(questText.gameObject, onComplete: "onQuestCompleted");
    }

    void fadeInOut(GameObject fadeObj, float alpha = 0f, string onComplete = null)
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", alpha);
        ht.Add("time", 0.5f);
        if (onComplete != null) {
            ht.Add("oncompletetarget", gameObject);
            ht.Add("oncomplete", onComplete);
        }
        iTween.FadeTo(fadeObj, ht);
    }

    void onQuestCompleted()
    {
        onAnimationCompleteDelegate();
    }

    void onNewQuestPlaced()
    {
        onAnimationNewQuestDelegate();
    }

    public void setCheckMarkAppear(bool isAppear)
    {
        Vector3 size = isAppear ? Vector3.one : Vector3.zero;
        checkMark.transform.localScale = size;
    }
}
