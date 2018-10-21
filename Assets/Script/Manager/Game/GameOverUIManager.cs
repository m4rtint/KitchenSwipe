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

    [SerializeField]
    GameObject homeButton;

    [SerializeField]
    GameObject playButton;

    ScoreManager scoreManager;
    #region mono
    void Awake()
    {
        scoreManager = ScoreManager.instance;
        setupObjects();
    }

    void setupObjects()
    {
        hs_Score.transform.localScale = Vector3.zero;
        score.transform.localScale = Vector3.zero;

        hs_Combo.transform.localScale = Vector3.zero;
        combo.transform.localScale = Vector3.zero;

        hs_Dishes.transform.localScale = Vector3.zero;
        dishes.transform.localScale = Vector3.zero;

        hs_TimeLasted.transform.localScale = Vector3.zero;
        timeLasted.transform.localScale = Vector3.zero;

        homeButton.transform.localScale = Vector3.zero;
        playButton.transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        startFadeIn();
        setupScoreText();
    }
    #endregion

    #region animation
    void startFadeIn()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("time", AnimationManager.instance.GameOverFadeInTime());
        ht.Add("oncomplete", "startPopOut");
        iTween.FadeFrom(gameObject, ht);
    }

    IEnumerator startPopOut()
    {
        popScore();
        yield return new WaitForSeconds(0.1f);
        popCombo();
        yield return new WaitForSeconds(0.1f);
        popDishes();
        yield return new WaitForSeconds(0.1f);
        popTime();
        yield return new WaitForSeconds(0.1f);
        popButtons();
    }


    void popScore()
    {
        popGameObject(score);
        popGameObject(hs_Score);
    }

    void popCombo()
    {
        popGameObject(combo);
        popGameObject(hs_Combo);
    }

    void popDishes()
    {
        popGameObject(dishes);
        popGameObject(hs_Dishes);
    }

    void popTime()
    {
        popGameObject(timeLasted);
        popGameObject(hs_TimeLasted);
    }

    void popButtons()
    {
        popGameObject(homeButton);
        popGameObject(playButton);
    }

    void popGameObject(GameObject obj)
    {
        Hashtable ht = new Hashtable();
        ht.Add("scale", Vector3.one);
        ht.Add("time", 0.5f);
        ht.Add("easeType", "easeOutBack");
        iTween.ScaleTo(obj, ht);
    }
    #endregion

    #region SetupText
    void setupScoreText()
    {
        setupScore();
        setupDishes();
        setupCombo();
        setupTimeLasted();
    } 

    void setupScore()
    {
        setTextMesh(hs_Score, scoreManager.HighScore(), true);
        setTextMesh(score, scoreManager.Score());
    }

    void setupDishes()
    {
        setTextMesh(hs_Dishes, scoreManager.HighScoreDishes(), true);
        setTextMesh(dishes, scoreManager.Dishes());
    }

    void setupCombo()
    {
        setTextMesh(hs_Combo, scoreManager.HighScoreCombo(), true);
        setTextMesh(combo, scoreManager.MaxCombo());
    }

    void setupTimeLasted()
    {
        setTextMesh(hs_TimeLasted, scoreManager.HighScoreSecondsLasted(), true);
        setTextMesh(timeLasted, TimeManager.instance.SecondsLasted());
    }
    #endregion

    #region helper
    void setTextMesh(GameObject go, int points, bool isHighScore = false)
    {
        string text = points.ToString();
        if (isHighScore) {
            text = "<color=black>~</color>" + text;
        }

        go.GetComponent<TextMeshProUGUI>().text = text;
    }
    #endregion
}
