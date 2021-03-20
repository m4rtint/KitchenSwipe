using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    //Delegate
    public delegate void ScoreDelegate();
    public ScoreDelegate scoreDelegate;
    public ScoreDelegate comboDelegate;

    public static ScoreManager instance = null;

    [SerializeField]
    float baseScore;
    [Header("Score Multiplier")]
    [SerializeField]
    float scoreMultiplier;
    [SerializeField]
    int comboStartNumber;

    [Header("Score Animation")]
    [SerializeField]
    int scoreSpeed;
    int finalScore;
    int score = 0;

    int maxCombo = 0;
    int combo = 0;
    int dishes = 0;

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
        return finalScore;
    }

    public int AnimatedScore()
    {
        return score;
    }

    public int Dishes()
    {
        return dishes;
    }

    public int Combo()
    {
        return combo;
    }

    public int MaxCombo()
    {
        return maxCombo;
    }

    public void resetCombo()
    {
        saveHighestComboIfNeeded();
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
        QuestManager.instance.checkCombo(combo);
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
        finalScore = score;
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
        if (finalScore - score < scoreSpeed && finalScore != score) 
        {
            score = finalScore;
            this.scoreDelegate();
        }
        else if (finalScore > score)
        {
            score += scoreSpeed;
            this.scoreDelegate();
        }
        
    }

    #endregion

    #region Score
    public int incrementScore()
    {
        onFinishDish();
        incrementCombo();
        int incrementingScore = (int)(baseScore * scoreMultiplier);
        incrementFinalScore(incrementingScore);
        return incrementingScore;
    }

    void onFinishDish()
    {
        dishes++;
        if (QuestManager.instance != null)
        {
            QuestManager.instance.checkDishes(dishes);
        }
    }

    public void saveScore()
    {
        if (Score() > HighScore())
        {
            uploadHighscore();
        }

        if (MaxCombo() > HighScoreCombo())
        {
            uploadCombo();
        }

        if (Dishes() > HighScoreDishes())
        {
            uploadDishes();
        }

        int time = TimeManager.instance.SecondsLasted();
        if (time > HighScoreSecondsLasted())
        {
            uploadTime();
        }
    }

    public void incrementFinalScore(int score)
    {
        finalScore += score;
    }

    void saveHighestComboIfNeeded(){
        if (Combo() > MaxCombo()) {
            maxCombo = combo;
        }
    }

    void uploadHighscore()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SCORE, Score());
    }


    void uploadDishes()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_DISHES, Dishes());
    }

    void uploadTime()
    {
        int time = TimeManager.instance.SecondsLasted();
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SECONDS, time);
    }

    void uploadCombo()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_COMBO, MaxCombo());
    }
    #endregion

    #region playerprefs
    public int HighScore()
    {
        return StoredValues(PlayerPrefKeys.INFINITE_SCORE);
    }

    public int HighScoreDishes()
    {
        return StoredValues(PlayerPrefKeys.INFINITE_DISHES);
    }

    public int HighScoreCombo()
    {
        return StoredValues(PlayerPrefKeys.INFINITE_COMBO);
    }

    public int HighScoreSecondsLasted()
    {
        return StoredValues(PlayerPrefKeys.INFINITE_SECONDS);
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
