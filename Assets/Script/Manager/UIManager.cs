using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    //Dependencies
    TimeManager m_TimeManager;

    [SerializeField]
    GameObject m_FadedBackgroundObject;
    [SerializeField]
    GameObject m_GameOverObject;
    [SerializeField]
    GameObject m_PauseScreenObject;



    #region Mono
    private void Awake()
    {
        InitManagers();
        InitDelegate();
    }

    void InitManagers()
    {
        m_TimeManager = TimeManager.instance;
    }

    void InitDelegate()
    {
        m_TimeManager.CheckGameOverDelegate += ShowGameOverScreen;
    }
    #endregion

    #region DelegateMethods
    void ShowGameOverScreen()
    {
        m_GameOverObject.SetActive(true);
        m_FadedBackgroundObject.SetActive(true);
    }
    #endregion

    #region GameOverPanel

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
