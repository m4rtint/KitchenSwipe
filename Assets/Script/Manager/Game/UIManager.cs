﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    //Dependencies
    ScoreManager scoreManager;

    [SerializeField]
    GameObject fadedBackgroundObject;
    [SerializeField]
    GameObject gameOverObject;
    [SerializeField]
    GameObject pauseScreenObject;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    GameObject comboTextObject;
    [SerializeField]
    Text dishesText;

    //StoreStateWhilePaused
    GameState pausedSavedState;


    #region Mono
    private void Awake()
    {
        
        initManagers();
        if (scoreManager != null) {
            initDelegate();
        }
        initializeUIText();
    }

    void initManagers()
    {
        scoreManager = ScoreManager.instance;
    }

    void initDelegate()
    {
        scoreManager.scoreDelegate += updateScore;
        if (comboTextObject != null) {
            scoreManager.comboDelegate += updateCombo;
        }
    }

    void initializeUIText()
    {
        if (scoreText != null) {
            scoreText.text = "000000000";
            dishesText.text = "0 DISHES";
        }
        if (comboTextObject != null) {
            comboTextObject.SetActive(false);
        }
    }
    #endregion

    #region DelegateMethods
    void updateScore(){
        if (scoreText != null)
        {
            scoreText.text = scoreManager.Score().ToString();
            dishesText.text = scoreManager.Dishes() + " Dishes";
        }
    }

    void updateCombo(){
        if (scoreManager.Combo() == 0) {
            comboTextObject.SetActive(false);
            return;
        }

        comboTextObject.SetActive(true);
        comboTextObject.GetComponent<ComboManager>().animateCombo(scoreManager.Combo());
    }
    #endregion

    #region Pause Panels

    public void showPauseScreen()
    {
        StateManager.instance.pauseGame();
        pauseScreenObject.SetActive(true);
        fadedBackgroundObject.SetActive(true);
    }

    public void hidePauseScreen()
    {
        StateManager.instance.unPausedGame();
        pauseScreenObject.SetActive(false);
        fadedBackgroundObject.SetActive(false);
    }

    public void startGameOverScreen()
    {
        gameOverObject.SetActive(true);
    }

    #endregion
}
