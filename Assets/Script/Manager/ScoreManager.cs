﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    //Delegate
    public delegate void ScoreDelegate();
    public ScoreDelegate scoreDelegate;
    public ScoreDelegate comboDelegate;

    //Player Pref keys
    readonly string INFINITE_MODE_SCORE = "Infinite";
    readonly string INFINITE_MODE_PLATE = "Infinte_Plate";

    public static ScoreManager instance = null;

    [SerializeField]
    float baseScore;
    [Header("Score Multiplier")]
    [SerializeField]
    float scoreMultiplier;
    [Header("Decrement Score Variable")]
    [SerializeField]
    float decrementScoreVariable;
    [SerializeField]
    int comboStartNumber;

    [SerializeField]
    int scoreSpeed;
    int reachingNumber;
    int score = 0;

    int combo = 0;
    int plates = 0;

    #region GetterSetter
    public void setScoreMultiplier(float var)
    {
        scoreMultiplier = var;
    }

	public void addToScoreMultiplier(float score){
		scoreMultiplier += score;
	}

    public int Score()
    {
        return score;
    }

    public int Plates()
    {
        return plates;
    }

    public int Combo()
    {
        return combo;
    }

    public void resetCombo()
    {
        combo = 0;
        this.comboDelegate();
    }

    void incrementCombo()
    {
        combo++;
        if (combo >= comboStartNumber)
        {
            this.comboDelegate();
        }
    }
    #endregion

    #region Mono
    private void Awake()
    {   
        instance = this;
        setupScoreProperties();
    }

    void setupScoreProperties()
    {
        reachingNumber = score;
        if (scoreSpeed == 0)
        {
            scoreSpeed = 10;
        }
        if (comboStartNumber == 0)
        {
            comboStartNumber = 1;
        }

    }

    private void Update()
    {
        animateChangeScore();
    }

    void animateChangeScore()
    {
        if (reachingNumber - score < scoreSpeed && reachingNumber != score) 
        {
            score = reachingNumber;
            this.scoreDelegate();
        } else if (reachingNumber < score)
        {
            score -= scoreSpeed;
            this.scoreDelegate();
        }
        else if (reachingNumber > score)
        {
            score += scoreSpeed;
            this.scoreDelegate();
        }
    }

    #endregion

    #region Score
    public int incrementScore()
    {
        plates++;
        incrementCombo();
        int incrementingScore = (int)(baseScore * scoreMultiplier);        
        reachingNumber += incrementingScore;
        return incrementingScore;
    }

    public void decrementScore()
    {
        reachingNumber -= (int)(baseScore * decrementScoreVariable);
    }

    public void saveScore()
    {
        if (score > HighScore())
        {
            PlayerPrefs.SetInt(INFINITE_MODE_SCORE, score);

        }

        if (plates > GetHighScorePlate())
        {
            PlayerPrefs.SetInt(INFINITE_MODE_PLATE, plates);
        }
		FirebaseDB.instance.InsertScoreEntry (score, plates);
    }
    #endregion

    #region playerprefs
    public int HighScore()
    {
        return StoredValues(INFINITE_MODE_SCORE);
    }

    public int GetHighScorePlate()
    {
        return StoredValues(INFINITE_MODE_PLATE);
    }

    int StoredValues(string key)
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
