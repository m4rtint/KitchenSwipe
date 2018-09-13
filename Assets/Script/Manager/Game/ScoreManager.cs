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
    [Header("Decrement Score Variable")]
    [SerializeField]
    float decrementScoreVariable;
    [SerializeField]
    int comboStartNumber;

    [SerializeField]
    int scoreSpeed;
    int reachingNumber;
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
        dishes++;
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
            saveHighScresToPlayerPrefs();
            uploadHighscore();
        }
    }

    void saveHighestComboIfNeeded(){
        if (Combo() > MaxCombo()) {
            maxCombo = combo;
            Debug.Log("Max Combo :" + maxCombo);
        }
    }

    void saveHighScresToPlayerPrefs()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SCORE, score);
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_DISHES, dishes);
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_COMBO, maxCombo);
        int time = TimeManager.instance.SecondsLasted();
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SECONDS, time);
    }

    void uploadHighscore()
    {
        FirebaseDB.instance.insertScoreEntry(HighScore(), HighScoreDishes(), HighScoreCombo(), HighScoreSecondsLasted());
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
