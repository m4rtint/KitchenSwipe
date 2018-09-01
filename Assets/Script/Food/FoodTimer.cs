using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FoodHolder))]
public class FoodTimer : MonoBehaviour {

    Color m_HundredPercent;
    Color m_FourtyPercent;
    Color m_TwentyPercent;

    //Dependencies
    FoodHolder m_FoodHolder;

    [SerializeField]
    GameObject m_TimerObject;
    float m_SecondsToComplete;

    [SerializeField]
    GameObject m_RedTimerObject;
    float m_RedSecondsToComplete;
    readonly float m_SpedUpSpeed = 10;



    #region mono
    private void Awake()
    {
        m_FoodHolder = GetComponent<FoodHolder>();
        m_HundredPercent = GetConvertColor(109, 255, 0);
        m_FourtyPercent = GetConvertColor(250, 138, 2);
        m_TwentyPercent = GetConvertColor(255, 3, 0);
    }

    private void Start()
    {
        ResetFoodTimerIfNeeded();
    }
    #endregion

    #region InitFoodTimer
    public void ResetFoodTimerIfNeeded()
    {
        if (m_FoodHolder.GetStoredFood() != null)
        {
            m_SecondsToComplete = m_FoodHolder.GetStoredFood().GetSecondsToComplete();
            m_RedSecondsToComplete = m_SecondsToComplete;
        }
    }
    #endregion

    #region gettersetter
    float FoodTime()
    {
        Food food = m_FoodHolder.GetStoredFood();
        if (food == null)
        {
            return -1;
        } else
        {
            return food.GetSecondsToComplete();
        }
    }

    public void DecrementTimeBy(float seconds)
    {
        m_SecondsToComplete -= seconds;
    }

    void DecrementRedTimeBy(float seconds)
    {
        if (m_RedSecondsToComplete > m_SecondsToComplete)
        {
            m_RedSecondsToComplete -= seconds*m_SpedUpSpeed;
        } else if (m_RedSecondsToComplete < m_SecondsToComplete)
        {
            m_RedSecondsToComplete = m_SecondsToComplete;
        }
        m_RedSecondsToComplete -= seconds;
    }

    float GetRatio(float seconds)
    {
        float ratio = 0;
        if (FoodTime() > 0 && m_SecondsToComplete >= 0)
        {
            ratio = seconds / FoodTime();
        }
        return ratio;
    }

    #endregion

    #region view
    public void UpdateTimer(float seconds)
    {
        if (m_FoodHolder.GetStoredFood() != null) {
            DecrementTimeBy(seconds);
            DecrementRedTimeBy(seconds);
            UpdateTimerUI();
            UpdateTimerColor();
            TimerAtZero();
        }
    }

    void UpdateTimerUI()
    {
        m_TimerObject.GetComponent<RectTransform>().localScale = new Vector3(GetRatio(m_SecondsToComplete), 1, 1);
        m_RedTimerObject.GetComponent<RectTransform>().localScale = new Vector3(GetRatio(m_RedSecondsToComplete), 1, 1);
    }

    void UpdateTimerColor()
    {
        float ratio = GetRatio(m_SecondsToComplete);
        if (ratio > 0.4)
        {
            m_TimerObject.GetComponent<Image>().color = m_HundredPercent;
        } else if(ratio > 0.2)
        {
            m_TimerObject.GetComponent<Image>().color = m_FourtyPercent;
        } else
        {
            m_TimerObject.GetComponent<Image>().color = m_TwentyPercent;
        }
    }

    void TimerAtZero()
    {
        if (m_SecondsToComplete <= 0)
        {
            ScoreManager.instance.decrementScore();
            TimeManager.instance.penaltyGameTime();
            ResetFoodTimerIfNeeded();
        }
    }
    #endregion

    #region tools
    Color GetConvertColor(float r, float g, float b)
    {
        float red = r / 255;
        float green = g / 255;
        float blue = b / 255;
        return new Color(red, green, blue);
    } 
    #endregion

}
