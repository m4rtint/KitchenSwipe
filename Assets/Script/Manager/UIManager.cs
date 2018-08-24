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
    GameObject m_PlatesTextObject;
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
        m_TimeManager.IsGameOverDelegate += ShowGameOverScreen;
    }

    void InitializeUIText()
    {
       m_ScoreTextObject.GetComponent<Text>().text = "SCORE\n0";
       m_PlatesTextObject.GetComponent<Text>().text = "PLATES\n0";
       m_ComboTextObject.SetActive(false);
    }
    #endregion

    #region DelegateMethods
    void ShowGameOverScreen()
    {
        m_GameOverObject.SetActive(true);
        m_FadedBackgroundObject.SetActive(true);
    }
    #endregion

    #region Game Interface
    void UpdateScore(){
        m_ScoreTextObject.GetComponent<Text>().text = "SCORE\n"+m_ScoreManager.GetScore();
        m_PlatesTextObject.GetComponent<Text>().text = "PLATES\n" + m_ScoreManager.GetPlates();
    }

    void UpdateCombo(){
        if (m_ScoreManager.GetCombo() == 0) {
            m_ComboTextObject.SetActive(false);
            return;
        }

        m_ComboTextObject.SetActive(true);
        m_ComboTextObject.GetComponent<Text>().text = "COMBO\n" + m_ScoreManager.GetCombo();
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
        StateManager.instance.PauseGame();
        m_PauseScreenObject.SetActive(true);
        m_FadedBackgroundObject.SetActive(true);
    }

    public void HidePauseScreen() {
        StateManager.instance.StartGame();
        m_PauseScreenObject.SetActive(false);
        m_FadedBackgroundObject.SetActive(false);
    }

    #endregion
}
