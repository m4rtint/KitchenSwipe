using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    //Delegate
    public delegate void ScoreDelegate();
    public ScoreDelegate thisDelegate;

    //Player Pref keys
    readonly string InfiniteMode = "Infinite";

    public static ScoreManager instance = null;

    [SerializeField]
    float BaseScore;
    [SerializeField]
    float m_IncrementScoreVariable;
    [SerializeField]
    float m_DecrementScoreVariable;

    int m_Score;

    #region GetterSetter
    public void SetScoreVariable(int var)
    {
        m_IncrementScoreVariable = var;
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
        m_Score += (int)(BaseScore * m_IncrementScoreVariable);
        this.thisDelegate();
    }

    public void DecrementScore()
    {
        m_Score -= (int)(BaseScore * m_DecrementScoreVariable);
        this.thisDelegate();
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
