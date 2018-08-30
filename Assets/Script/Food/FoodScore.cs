using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodScore : MonoBehaviour {

    [SerializeField]
    GameObject m_ScoreObject;
    TextMeshProUGUI m_ScoreText;
    
    AnimationManager animation;

    #region Mono
    private void Awake()
    {
        m_ScoreText = m_ScoreObject.GetComponent<TextMeshProUGUI>();
        animation = AnimationManager.instance;
        ResetScoreAnimation();
    }
    #endregion

    #region Animation
    public void RisingScoreAnimation(int score)
    {

        Hashtable ht = new Hashtable();
        ht.Add("delay", AnimationManager.instance.ScoreDelayTime());
        ht.Add("scale", new Vector3(1.2f, 1.2f, 0));
        ht.Add("time", AnimationManager.instance.ScorePopTime());
        ht.Add("easeType", "spring");

        //On Start function to call
        ht.Add("onstart", "SetScoreText");
        ht.Add("onstartparams",score);
        ht.Add("onstarttarget", gameObject);

        //On complete function to call
        ht.Add("oncompletetarget", gameObject);
        ht.Add("oncomplete", "ResetScoreAnimation");
        iTween.ScaleFrom(m_ScoreObject, ht);
    }
     
    void SetScoreText(int score)
    {
        m_ScoreText.text = score.ToString()+"pts";
    }

    void ResetScoreAnimation()
    {
        m_ScoreText.text = "";
    }
    #endregion
}
