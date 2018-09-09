using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    //Dependencies
    TimeManager m_TimeManager;
    ScoreManager m_ScoreManager;

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

    #region Mono
    private void Awake()
    {
        initManagers();
        initDelegate();
        initializeUIText();
    }

    void initManagers()
    {
        m_TimeManager = TimeManager.instance;
        m_ScoreManager = ScoreManager.instance;
    }

    void initDelegate()
    {
        m_ScoreManager.scoreDelegate += updateScore;
        m_ScoreManager.comboDelegate += updateCombo;
    }

    void initializeUIText()
    {
       scoreTextObject.GetComponent<Text>().text = "000000000\n0 Dishes";
       comboTextObject.SetActive(false);
    }
    #endregion

    #region DelegateMethods
    void updateScore(){
        scoreTextObject.GetComponent<Text>().text = m_ScoreManager.Score() + "\n"+ m_ScoreManager.Dishes() + " Dishes";
    }

    void updateCombo(){
        if (m_ScoreManager.Combo() == 0) {
            comboTextObject.SetActive(false);
            return;
        }

        comboTextObject.SetActive(true);
        comboTextObject.GetComponent<Text>().text = "COMBO\n" + m_ScoreManager.Combo();
        animateCombo();
    }

    void animateCombo(){
        Hashtable ht = new Hashtable();
        ht.Add("scale", new Vector3(1.2f, 1.2f, 0));
        ht.Add("time", AnimationManager.instance.ComboPopTime());
        ht.Add("easeType", "spring");
        iTween.ScaleFrom(comboTextObject, ht);
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
        StateManager.instance.startGame();
        pauseScreenObject.SetActive(false);
        fadedBackgroundObject.SetActive(false);
    }

    public void startGameOverScreen()
    {
        gameOverObject.SetActive(true);
    }

    #endregion
}
