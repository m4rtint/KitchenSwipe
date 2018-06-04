using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    //Player Pref keys
    readonly string InfiniteMode = "Infinite";

    public static ScoreManager instance = null;

    readonly int BaseScore = 100;
    int m_Score;
    int m_ScoreVariable;

    #region GetterSetter
    public void SetScoreVariable(int var)
    {
        m_ScoreVariable = var;
    }

    public int GetScore()
    {
        return m_Score;
    }
    #endregion

    #region Mono
    private void Awake()
    {   
        instance = this;
        m_Score = 0;
        SetupHighScore();
    }

    void SetupHighScore()
    {
        if (!PlayerPrefs.HasKey(InfiniteMode)) { 
            PlayerPrefs.SetInt(InfiniteMode, 0);
        }
    }
    #endregion

    #region Score
    public void IncrementScore()
    {
        m_Score += (BaseScore * m_ScoreVariable);
    }

    public void SaveScore()
    {
        if (m_Score > PlayerPrefs.GetInt(InfiniteMode))
        {
            PlayerPrefs.SetInt(InfiniteMode, m_Score);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(InfiniteMode);
    }
    #endregion
}
