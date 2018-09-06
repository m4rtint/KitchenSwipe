using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodScore : MonoBehaviour {

    [SerializeField]
    GameObject scoreObject;
    TextMeshProUGUI scoreText;
    
    AnimationManager animation;

    #region Mono
    private void Awake()
    {
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        animation = AnimationManager.instance;
        resetScoreAnimation();
    }
    #endregion

    #region Animation
    public void risingScoreAnimation(int score)
    {

        Hashtable ht = new Hashtable();
        ht.Add("delay", AnimationManager.instance.ScoreDelayTime());
        ht.Add("scale", new Vector3(1.2f, 1.2f, 0));
        ht.Add("time", AnimationManager.instance.ScorePopTime());
        ht.Add("easeType", "spring");

        //On Start function to call
        ht.Add("onstart", "ScoreText");
        ht.Add("onstartparams",score);
        ht.Add("onstarttarget", gameObject);

        //On complete function to call
        ht.Add("oncompletetarget", gameObject);
        ht.Add("oncomplete", "resetScoreAnimation");
        iTween.ScaleFrom(scoreObject, ht);
    }
     
    void ScoreText(int score)
    {
        scoreText.text = score.ToString()+"pts";
    }

    void resetScoreAnimation()
    {
        scoreText.text = "";
    }
    #endregion
}
