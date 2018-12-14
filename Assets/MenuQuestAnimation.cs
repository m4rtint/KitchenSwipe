using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuQuestAnimation : MonoBehaviour {

    public delegate void MenuQuestDelegate();
    public MenuQuestDelegate onAnimationCompleteDelegate;

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
    }

    public void questCompleteAnimation()
    {
        setCheckMarkAppear(true);

        Hashtable ht = new Hashtable();
        ht.Add("scale", Vector3.one * 3);
        ht.Add("easetype", "easeOutBack");
        ht.Add("time", 1f);
        ht.Add("oncomplete", "onAnimationCompleteDelegate");
        iTween.ScaleFrom(checkMark, ht);
    }

    public void setCheckMarkAppear(bool isAppear)
    {
        Vector3 size = isAppear ? Vector3.one : Vector3.zero;
        checkMark.transform.localScale = size;
    }
}
