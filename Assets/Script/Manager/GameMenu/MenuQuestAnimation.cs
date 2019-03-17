using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        scaleFrom("onNewQuestPlaced", questText.gameObject);
    }

    public void questCompleteAnimation()
    {
        setCheckMarkAppear(true);
        scaleFrom("onQuestCompleted", checkMark);
    }


    void scaleFrom(string onComplete, GameObject scaleObj)
    {
        Hashtable ht = new Hashtable();
        ht.Add("scale", Vector3.one * 3);
        ht.Add("easetype", "easeOutBack");
        ht.Add("time", 1f);
        if (onComplete.Length > 0)
        {
            ht.Add("oncompletetarget", gameObject);
            ht.Add("oncomplete", onComplete);
        }
        iTween.ScaleFrom(scaleObj, ht);
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
