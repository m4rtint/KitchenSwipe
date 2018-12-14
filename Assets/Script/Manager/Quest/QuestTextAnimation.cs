using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestTextAnimation : MonoBehaviour {

    Queue<Quest> questStateChanges = new Queue<Quest>();
    Quest currentQuest;

    bool isAnimationRunning = false;
    Text questText;
    Color questTextColor;

    AnimationManager animation;

    [SerializeField]
    TextMeshProUGUI pointsText;
    Vector3 pointsLocation;

    void Awake()
    {
        questText = GetComponent<Text>();
        setQuestText();
        setPointText();
        pointsLocation = pointsText.gameObject.transform.position;
        animation = AnimationManager.instance;
    }
    #region Setter
    void setQuestText(string text = "")
    {
        GetComponent<Text>().text = text;
    }

    void setPointText(int points = -1)
    {
        string text = "";
        if (points != -1)
        {
            text = "+" + points.ToString() + "Points";
        }
        pointsText.text = text;
    }
    #endregion

    #region public
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
                    updateQuestText(true);
                    onStartQuestCompleteAnimation();
                    break;
                case QuestState.NEW:
                    questTextColor = Color.grey;
                    updateQuestText(false);
                    onStartNewQuestAnimation();
                    break;
            }
        }
    }

    void completedAnimation()
    {
        isAnimationRunning = false;
        resetPointsPosition();
        startQuestAnimationIfNeeded();
    }

    void updateQuestText(bool isPointsNeeded)
    {
        int points = isPointsNeeded ? currentQuest.getQuestPoints() : -1;
        setPointText(points);
        setQuestText(currentQuest.GetQuestText());
        questText.color = questTextColor;
        pointsText.color = questTextColor;
    }

    #region Animation

    void onStartNewQuestAnimation()
    {
        fadeIn();
        moveUpToOriginal();
        fadeOut(gameObject);
    }

    void onStartQuestCompleteAnimation()
    {
        fadeIn();
        moveUpToOriginal();
        moveUpFromOriginal();

        fadeOut(gameObject, false);
        fadeOut(pointsText.gameObject);
    }

    void fadeIn()
    {
        Debug.Log("Animation: Fade In");
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 1);
        ht.Add("time", animation.QuestRiseUpTime());
        iTween.FadeTo(gameObject, ht);
    }

    void moveUpToOriginal(string oncompleted = null)
    {
        Debug.Log("Animation: Move Up To Original");
        Hashtable ht = new Hashtable();
        ht.Add("position", new Vector3(0, -100, 0));
        ht.Add("easetype", "easeOutCubic");
        ht.Add("time", animation.QuestRiseUpTime());
        if (oncompleted != null)
        {
            ht.Add("oncomplete", oncompleted);
        }
        iTween.MoveFrom(gameObject, ht);
    }

    void moveUpFromOriginal()
    {
        Debug.Log("Animation: Move Up From Original");
        Hashtable ht = new Hashtable();
        ht.Add("time", animation.QuestRiseUpTime());
        ht.Add("delay", animation.QuestRiseUpTime() + animation.QuestDelayTime());
        ht.Add("easetype", "easeOutCubic");
        ht.Add("y", 200);
        iTween.MoveAdd(pointsText.gameObject, ht);
    }

    void fadeOut(GameObject obj, bool isNeedOnComplete = true)
    {
        Debug.Log("Animation: Fade Out");
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("delay", animation.QuestRiseUpTime()+ animation.QuestDelayTime());
        ht.Add("time", animation.QuestRiseUpTime());
        ht.Add("includechildren", false);
        if (isNeedOnComplete) {
            ht.Add("oncompletetarget", gameObject);
            ht.Add("oncomplete", "completedAnimation");
        }
        iTween.FadeTo(obj, ht);
    }
    #endregion

    #region positioning
    void resetPointsPosition()
    {
        pointsText.gameObject.transform.position = pointsLocation;
    } 
    #endregion


}
