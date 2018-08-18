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
    [Header("Increment Score Variable")]
    [SerializeField]
    float m_IncrementScoreVariable;
    [Header("Decrement Score Variable")]
    [SerializeField]
    float m_DecrementScoreVariable;
    [SerializeField]
    int m_ComboStartNumber;

    [SerializeField]
    int m_ScoreSpeed;
    int reachingNumber;
    int m_Score = 0;

    int m_Combo = 0;
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

    public int GetCombo()
    {
        return m_Combo;
    }

    public void ResetCombo()
    {
        m_Combo = 0;
        this.comboDelegate();
    }

    void IncrementCombo()
    {
        m_Combo++;
        if (m_Combo >= m_ComboStartNumber)
        {
            this.comboDelegate();
        }
    }
    #endregion

    #region Mono
    private void Awake()
    {   
        instance = this;
        SetupScoreProperties();
    }

    void SetupScoreProperties()
    {
        reachingNumber = m_Score;
        if (m_ScoreSpeed == 0)
        {
            m_ScoreSpeed = 10;
        }
        if (m_ComboStartNumber == 0)
        {
            m_ComboStartNumber = 1;
        }

    }

    private void Update()
    {
        AnimateChangeScore();
    }

    void AnimateChangeScore()
    {
        if (reachingNumber - m_Score < m_ScoreSpeed && reachingNumber != m_Score) 
        {
            m_Score = reachingNumber;
            this.scoreDelegate();
        } else if (reachingNumber < m_Score)
        {
            m_Score -= m_ScoreSpeed;
            this.scoreDelegate();
        }
        else if (reachingNumber > m_Score)
        {
            m_Score += m_ScoreSpeed;
            this.scoreDelegate();
        }
    }

    #endregion

    #region Score
    public int IncrementScore()
    {
        m_Plates++;
        IncrementCombo();
        int score = (int)(BaseScore * m_IncrementScoreVariable);        
        reachingNumber += score;
        return score;
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
