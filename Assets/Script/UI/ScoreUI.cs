using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    Text m_ScoreText;
    ScoreManager m_ScoreManager;

    // Use this for initialization
    void Awake() {
        InitScoreUI();
        InitDelegate();
    }

    void InitScoreUI()
    {
        m_ScoreText = GetComponent<Text>();
        m_ScoreManager = ScoreManager.instance;
    }

    void InitDelegate()
    {
        m_ScoreManager.thisDelegate += UpdateScore;
    }

    void UpdateScore()
    {
        m_ScoreText.text = m_ScoreManager.GetScore().ToString();
    }
	
}
