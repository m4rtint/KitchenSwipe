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
        m_TimeManager.CheckGameOverDelegate += ShowGameOverScreen;
    }

    void InitializeUIText()
    {
       m_ScoreTextObject.GetComponent<Text>().text = "SCORE\n0";
       m_PlatesTextObject.GetComponent<Text>().text = "PLATES\n0";
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
