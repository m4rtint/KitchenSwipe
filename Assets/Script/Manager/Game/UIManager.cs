using System.Collections;
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
    GameObject scoreTextObject;
    [SerializeField]
    GameObject comboTextObject;

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
       scoreTextObject.GetComponent<Text>().text = "000000000\n0 Dishes";
        if (comboTextObject != null) {
            comboTextObject.SetActive(false);
        }
    }
    #endregion

    #region DelegateMethods
    void updateScore(){
        scoreTextObject.GetComponent<Text>().text = scoreManager.Score() + "\n"+ scoreManager.Dishes() + " Dishes";
    }

    void updateCombo(){
        if (scoreManager.Combo() == 0) {
            comboTextObject.SetActive(false);
            return;
        }

        comboTextObject.SetActive(true);
        comboTextObject.GetComponent<ComboManager>().setComboText("x" + scoreManager.Combo());
        animateCombo();
    }

    void animateCombo(){
        Hashtable ht = new Hashtable();
        ht.Add("scale", AnimationManager.instance.ComboPopScale());
        ht.Add("time", AnimationManager.instance.ComboPopTime());
        ht.Add("easeType", "easeInQuart");
        iTween.ScaleFrom(comboTextObject.GetComponent<ComboManager>().getTextObject(), ht);
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
