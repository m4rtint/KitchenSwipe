using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeUI : MonoBehaviour {

    Text m_TimerText;
    TimeManager m_TimeManager;

    #region Mono
    // Use this for initialization
    void Awake () {
        InitTimerUI();
        InitDelegate();
    }


    void InitDelegate()
    {
        m_TimeManager.thisDelegate += UpdateTime;
    }

    void InitTimerUI()
    {
        m_TimerText = GetComponent<Text>();
        m_TimeManager = TimeManager.instance;
    }
    #endregion
    #region UI

    // Update is called once per frame
    void UpdateTime () {
        m_TimerText.text = m_TimeManager.GameTime();
    }
#endregion
}
