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
        } else if (finalScore < score)
        {
            score -= scoreSpeed;
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
        dishes++;
        incrementCombo();
        int incrementingScore = (int)(baseScore * scoreMultiplier);
        incrementFinalScore(incrementingScore);
        return incrementingScore;
    }

    public void incrementFinalScore(int score)
    {
        finalScore += score;
    }

    public void decrementScore()
    {
        finalScore -= (int)(baseScore * decrementScoreVariable);
    }

    public void saveScore()
    {
        if (FirebaseDB.instance != null) {
            int s = Score();
            int hs_score = HighScore();
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

        uploadAnalyticsIfNeeded();
    }

    void uploadAnalyticsIfNeeded() {
        if (FbAnalytics.instance != null && Score() > 0)
        {
            FbAnalytics.instance.gameResult(Score(), Dishes(), TimeManager.instance.SecondsLasted(), MaxCombo());
        }
    }

    void saveHighestComboIfNeeded(){
        if (Combo() > MaxCombo()) {
            maxCombo = combo;
        }
    }

    void uploadHighscore()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SCORE, Score());
        FirebaseDB.instance.insertScoreEntry(HighScore());
    }

    void uploadDishes()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_DISHES, Dishes());
        FirebaseDB.instance.insertDishesEntry(HighScoreDishes());
    }

    void uploadTime()
    {
        int time = TimeManager.instance.SecondsLasted();
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SECONDS, time);
        FirebaseDB.instance.insertTimeEntry(HighScoreSecondsLasted());
    }

    void uploadCombo()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_COMBO, MaxCombo());
        FirebaseDB.instance.insertComboEntry(HighScoreCombo());
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
