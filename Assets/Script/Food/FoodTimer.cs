using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FoodHolder))]
public class FoodTimer : MonoBehaviour {

    float m_SecondsToComplete;
    Color m_HundredPercent;
    Color m_FourtyPercent;
    Color m_TwentyPercent;

    //Dependencies
    FoodHolder m_FoodHolder;

    [SerializeField]
    GameObject m_TimerObject;

    #region mono
    private void Awake()
    {
        m_FoodHolder = GetComponent<FoodHolder>();
    }

    private void Start()
    {
        ResetFoodTimer();
        m_HundredPercent = GetConvertColor(109, 255, 0);
        m_FourtyPercent = GetConvertColor(250,138,2);
        m_TwentyPercent = GetConvertColor(255,3,0);
    }
    #endregion

    #region InitFoodTimer
    void ResetFoodTimer()
    {
        m_SecondsToComplete = m_FoodHolder.GetStoredFood().GetSecondsToComplete();
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

    float GetRatio()
    {
        float ratio = 0;
        if (FoodTime() > 0)
        {
            ratio = m_SecondsToComplete / FoodTime();
        }
        return ratio;
    }
    #endregion

    #region view
    public void UpdateTimer(float seconds)
    {
        DecrementTimeBy(seconds);
        UpdateTimerUI();
        UpdateTimerColor();
        TimerAtZero();
    }

    void UpdateTimerUI()
    {
        m_TimerObject.GetComponent<RectTransform>().localScale = new Vector3(GetRatio(), 1, 1);
    }

    void UpdateTimerColor()
    {
        float ratio = GetRatio();
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
            ScoreManager.instance.DecrementScore();
            TimeManager.instance.PenaltyGameTime();
            ResetFoodTimer();
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
