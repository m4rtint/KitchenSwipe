using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    //Delegate
    public delegate void ScoreDelegate();
    public ScoreDelegate scoreDelegate;
    public ScoreDelegate comboDelegate;

    //Player Pref keys
    readonly string InfiniteMode = "Infinite";
    readonly string InfiniteMode_Plates = "Infinte_Plate";

    public static ScoreManager instance = null;

    [SerializeField]
    float BaseScore;
    [SerializeField]
    float m_IncrementScoreVariable;
    [SerializeField]
    float m_DecrementScoreVariable;

    [SerializeField]
    int m_ScoreSpeed;
    int reachingNumber;
    int m_Score = 0;

    int combo = 0;
    int m_Plates = 0;

    #region GetterSetter
    public void SetIncrementScoreVariable(float var)
    {
        m_IncrementScoreVariable = var;
    }

	public void AddToIncrementScoreVariable(float score){
		m_IncrementScoreVariable += score;
	}

	public float IncrementScoreVariable(){
		return m_IncrementScoreVariable;
	}

    public int GetScore()
    {
        return m_Score;
    }

    public int GetPlates()
    {
        return m_Plates;
    }

    void ResetCombo()
    {
        combo = 0;
    }

    void IncrementCombo()
    {
        combo++;
        if (combo >= 4)
        {
            this.comboDelegate();
        }
    }
    #endregion

    #region Mono
    private void Awake()
    {   
        instance = this;
        reachingNumber = m_Score;
    }

    private void Update()
    {
        AnimateChangeScore();
    }

    void AnimateChangeScore()
    {

        if (reachingNumber < m_Score)
        {
            m_Score -= m_ScoreSpeed;
            this.scoreDelegate();
        } else if (reachingNumber > m_Score)
        {
            m_Score += m_ScoreSpeed;
            this.scoreDelegate();

        } else if((reachingNumber-m_Score) < m_ScoreSpeed && reachingNumber != m_Score)
        {
            m_Score = reachingNumber;
            this.scoreDelegate();
        }
    }

    #endregion

    #region Score
    public void IncrementScore()
    {
        m_Plates++;
        IncrementCombo();
        reachingNumber += (int)(BaseScore * m_IncrementScoreVariable);
    }

    public void DecrementScore()
    {
        reachingNumber -= (int)(BaseScore * m_DecrementScoreVariable);
    }

    public void SaveScore()
    {
        if (m_Score > GetHighScore())
        {
            PlayerPrefs.SetInt(InfiniteMode, m_Score);
        }

        if (m_Plates > GetHighScorePlate())
        {
            PlayerPrefs.SetInt(InfiniteMode_Plates, m_Plates);
        }
    }

    public int GetHighScore()
    {
        return GetStoredScoresWith(InfiniteMode);
    }

    public int GetHighScorePlate()
    {
        return GetStoredScoresWith(InfiniteMode_Plates);
    }

    int GetStoredScoresWith(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 0);
            return 0;
        }
        return PlayerPrefs.GetInt(key);
    }
    #endregion
}
