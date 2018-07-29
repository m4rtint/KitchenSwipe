using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    [SerializeField]
    GameObject m_RingLayerObject;
    Image m_RingImage;

    [SerializeField]
    GameObject m_TimeTextObject;
    Text m_TimeText;

    //Dependencies
    TimeManager m_TimeManager;

    float m_FullTime = -1;

    #region Mono
    private void Awake()
    {
        m_RingImage = m_RingLayerObject.GetComponent<Image>();
        m_TimeText = m_TimeTextObject.GetComponent<Text>();
        m_TimeManager = TimeManager.instance;
        InitDelegate();
    }

    private void Start()
    {
        InitTimer();
    }

    void InitTimer()
    {
        if (m_FullTime == -1)
        {
            m_FullTime = m_TimeManager.GameTimeF();
        }
    }

    void InitDelegate()
    {
        m_TimeManager.UpdateTimerUIDelegate += UpdateText;
        m_TimeManager.UpdateTimerUIDelegate += UpdateRing;
    }

    void Update()
    {
        UpdateText();
        UpdateRing();
    }
    #endregion

    #region UI
    void UpdateText()
    {
        m_TimeText.text = m_TimeManager.GameTime();
    }

    void UpdateRing()
    {
        m_RingImage.fillAmount = m_TimeManager.GameTimeF() / m_FullTime;
    }
    #endregion




}
