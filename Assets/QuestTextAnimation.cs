using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTextAnimation : MonoBehaviour {

    Queue<Quest> questStateChanges = new Queue<Quest>();
    Quest currentQuest;

    bool isAnimationRunning = false;
    Text questText;
    Color questTextColor;

    void Awake()
    {
        questText = GetComponent<Text>();
    }

    #region public
    public void setQuestText(string text)
    {
        questText.text = text;
    }

    public void onStateChange(Quest q)
    {
        questStateChanges.Enqueue(q);
        startQuestAnimationIfNeeded();
    }
    #endregion
    void startQuestAnimationIfNeeded()
    {
        if (questStateChanges.Count > 0 && !isAnimationRunning)
        {
            isAnimationRunning = true;
            currentQuest = questStateChanges.Dequeue();
            switch (currentQuest.State())
            {
                case QuestState.COMPLETE:
                    questTextColor = Color.green;
                    break;
                case QuestState.NEW:
                    questTextColor = Color.grey;
                    break;
            }

            startQuestAnimation();
        }
    }

    void completedAnimation()
    {
        isAnimationRunning = false;
        startQuestAnimationIfNeeded();
    }

    void updateQuestText()
    {
        setQuestText(currentQuest.getQuestText());
        gameObject.transform.localScale = Vector3.zero;
        questText.color = questTextColor;
    }

    #region Animation
    void startQuestAnimation()
    {
        updateQuestText();
        Hashtable ht = new Hashtable();
        ht.Add("time", 1f);
        ht.Add("scale", Vector3.one);
        ht.Add("easetype", "easeOutElastic");
        ht.Add("oncomplete", "fadeOut");
        iTween.ScaleTo(gameObject, ht);
    }

    void fadeOut()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("time", 0.5f);
        ht.Add("oncomplete", "completedAnimation");
        iTween.FadeTo(gameObject, ht);
    }
    #endregion


}
