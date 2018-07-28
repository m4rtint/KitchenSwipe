using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FoodHolder))]
public class FoodTimer : MonoBehaviour {

    float m_SecondsToComplete;

    //Dependencies
    FoodHolder m_FoodHolder;

    [SerializeField]
    GameObject m_TimerObject;

    #region mono
    private void Awake()
    {
        m_FoodHolder = GetComponent<FoodHolder>();
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
    #endregion
    
    #region view
    public void UpdateTimer(float seconds)
    {
        DecrementTimeBy(seconds);
        UpdateTimerUI();
        TimerAtZero();
    }

    void UpdateTimerUI()
    {
        float ratio = 0;
        if (FoodTime() > 0)
        {
            ratio = m_SecondsToComplete / FoodTime();
        } 
        m_TimerObject.GetComponent<RectTransform>().localScale = new Vector3(ratio, 1, 1);

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



}
