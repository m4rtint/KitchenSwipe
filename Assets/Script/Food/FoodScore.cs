using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodScore : MonoBehaviour {

    [SerializeField]
    GameObject m_ScoreObject;
    TextMeshProUGUI m_ScoreText;

    Vector3 originalPlacement;
    AnimationManager animation;

    #region Mono
    private void Awake()
    {
        m_ScoreText = m_ScoreObject.GetComponent<TextMeshProUGUI>();
        originalPlacement = m_ScoreObject.transform.position;
        animation = AnimationManager.instance;
        SetScoreActive(false);
    }

    void SetScoreActive(bool active)
    {
        m_ScoreObject.SetActive(active);
    }
    #endregion

    #region Animation
    public void RisingScoreAnimation(int score)
    {
        SetScoreText(score);

        Hashtable ht = new Hashtable();
        ht.Add("y", animation.ScoreRiseAmount());
        ht.Add("time", animation.ScoreRiseTime());
        ht.Add("easeType", "easeOutCubic");
        ht.Add("oncomplete", "ResetScoreAnimation");
        ht.Add("oncompletetarget", gameObject);
        iTween.MoveAdd(m_ScoreObject, ht);
    }
     
    void SetScoreText(int score)
    {
        SetScoreActive(true);
        m_ScoreText.text = score.ToString()+"pts";
    }

    void ResetScoreAnimation()
    {
        Debug.Log("Reset animation");
        m_ScoreObject.transform.position = originalPlacement;
        SetScoreActive(false);
    }
    #endregion
}
