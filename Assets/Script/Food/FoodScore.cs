using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodScore : MonoBehaviour {

    [SerializeField]
    GameObject scoreObject;
    TextMeshProUGUI scoreText;

    [SerializeField]
    GameObject penaltyObject;
    TextMeshProUGUI penaltyText;
    
    AnimationManager animation;

    #region Mono
    private void Awake()
    {
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        penaltyText = penaltyObject.GetComponent<TextMeshProUGUI>();
        animation = AnimationManager.instance;
        resetScoreText();
        resetTimeText();
    }
    #endregion

    #region Animation
    public void popScoreAnimation(int score)
    {
        ScoreText(score);

        Hashtable ht = new Hashtable();
        ht.Add("delay", AnimationManager.instance.ScoreDelayTime());
        ht.Add("scale", new Vector3(1.2f, 1.2f, 0));
        ht.Add("time", AnimationManager.instance.ScorePopTime());
        ht.Add("easeType", "spring");

        //On complete function to call
        ht.Add("oncompletetarget", gameObject);
        ht.Add("oncomplete", "resetScoreText");
        iTween.ScaleFrom(scoreObject, ht);
    }

    //Called in iTween animation
    void ScoreText(int score)
    {
        scoreText.text = score.ToString() + "pts";
    }

    public void risingLostTime()
    {
        int time = (int)TimeManager.instance.GameTimePenalty();
        TimeText(time);

        Hashtable ht = new Hashtable();
        ht.Add("y", animation.PenaltyRiseAmount());
        ht.Add("time", animation.PenaltyRiseTime());
        ht.Add("easeType", "easeOutExpo");

        //On complete function call
        ht.Add("oncompletetarget", gameObject);
        ht.Add("oncomplete", "resetTimeText");
        iTween.MoveAdd(penaltyObject, ht);
    }

    //Called in iTween animation
    void TimeText(int time)
    {
        penaltyText.text = "- " + time + " Seconds";
    }
     
    void resetTimeText()
    {
        penaltyText.text = "";
    }

    void resetScoreText()
    {
        scoreText.text = "";
    }
    #endregion
}
