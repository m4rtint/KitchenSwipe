using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUIManager : MonoBehaviour {

    [SerializeField]
    GameObject hs_Score;

    [SerializeField]
    GameObject score;

    [SerializeField]
    GameObject hs_Combo;

    [SerializeField]
    GameObject combo;

    [SerializeField]
    GameObject hs_Dishes;

    [SerializeField]
    GameObject dishes;

    [SerializeField]
    GameObject hs_TimeLasted;

    [SerializeField]
    GameObject timeLasted;

    ScoreManager scoreManager;
    #region mono
    void Awake()
    {
        scoreManager = ScoreManager.instance;
    }
    private void OnEnable()
    {
        startFadeIn();
        setupScoreText();
    }

    void startFadeIn()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("time", AnimationManager.instance.GameOverFadeInTime());
        iTween.FadeFrom(gameObject, ht);
    }

    void setupScoreText()
    {
        setupScore();
        setupDishes();
        setupCombo();
        setupTimeLasted();
    } 

    void setupScore()
    {
        setTextMesh(hs_Score, scoreManager.HighScore());
        setTextMesh(score, scoreManager.Score());
    }

    void setupDishes()
    {
        setTextMesh(hs_Dishes, scoreManager.HighScoreDishes());
        setTextMesh(dishes, scoreManager.Dishes());
    }

    void setupCombo()
    {
        setTextMesh(hs_Combo, scoreManager.HighScoreCombo());
        setTextMesh(dishes, scoreManager.Combo());
    }

    void setupTimeLasted()
    {
        setTextMesh(hs_TimeLasted, scoreManager.HighScoreSecondsLasted());
        setTextMesh(timeLasted, TimeManager.instance.SecondsLasted());
    }
    #endregion

    #region helper
    void setTextMesh(GameObject go, int points)
    {
        go.GetComponent<TextMeshProUGUI>().text = points.ToString();
    }
    #endregion
}
