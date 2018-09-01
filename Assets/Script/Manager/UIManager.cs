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
    GameObject m_FadedBackgroundObject;
    [SerializeField]
    GameObject m_GameOverObject;
    [SerializeField]
    GameObject m_PauseScreenObject;
    [SerializeField]
    GameObject m_ScoreTextObject;
    [SerializeField]
    GameObject m_ComboTextObject;

    #region Mono
    private void Awake()
    {
        InitManagers();
        InitDelegate();
        InitializeUIText();
    }

    void InitManagers()
    {
        m_TimeManager = TimeManager.instance;
        m_ScoreManager = ScoreManager.instance;
    }

    void InitDelegate()
    {
        m_ScoreManager.scoreDelegate += UpdateScore;
        m_ScoreManager.comboDelegate += UpdateCombo;
        m_TimeManager.isGameOverDelegate += ShowGameOverScreen;
    }

    void InitializeUIText()
    {
       m_ScoreTextObject.GetComponent<Text>().text = "000000000\n0 Dishes";
       m_ComboTextObject.SetActive(false);
    }
    #endregion

    #region DelegateMethods
    void ShowGameOverScreen()
    {
        m_GameOverObject.SetActive(true);
        m_FadedBackgroundObject.SetActive(true);
    }
 
    void UpdateScore(){
        m_ScoreTextObject.GetComponent<Text>().text = m_ScoreManager.Score() + "\n"+ m_ScoreManager.Plates() + " Dishes";
    }

    void UpdateCombo(){
        if (m_ScoreManager.Combo() == 0) {
            m_ComboTextObject.SetActive(false);
            return;
        }

        m_ComboTextObject.SetActive(true);
        m_ComboTextObject.GetComponent<Text>().text = "COMBO\n" + m_ScoreManager.Combo();
        AnimateCombo();
    }

    void AnimateCombo(){
        Hashtable ht = new Hashtable();
        ht.Add("scale", new Vector3(1.2f, 1.2f, 0));
        ht.Add("time", AnimationManager.instance.ComboPopTime());
        ht.Add("easeType", "spring");
        iTween.ScaleFrom(m_ComboTextObject, ht);
    }
    #endregion

    #region Pause Panels

    public void ShowPauseScreen()
    {
        StateManager.instance.pauseGame();
        m_PauseScreenObject.SetActive(true);
        m_FadedBackgroundObject.SetActive(true);
    }

    public void HidePauseScreen() {
        StateManager.instance.startGame();
        m_PauseScreenObject.SetActive(false);
        m_FadedBackgroundObject.SetActive(false);
    }

    #endregion
}
