﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodOrdered : MonoBehaviour
{
    Food m_Food;
	bool m_IsEmpty = true;
	float m_SecondsForComplete;
    float m_RecordedSecondsForComplete;

	#region Setter/Getter
	public float GetSecondsForComplete(){
		return m_SecondsForComplete;
	}

	public bool IsEmpty()
	{
		return m_IsEmpty;
	}

    public Food GetFood()
    {
        return m_Food;
    }
	#endregion


	#region View
	public void RemoveFood(){
        m_Food = null;
        m_SecondsForComplete = 0;
		m_IsEmpty = true;
	}

	public void UpdateOrderView(float seconds)
	{

		if (m_IsEmpty)
        {
            SetOrderEmptyView();
            return;
        }
		DecrementSecondsBy(seconds);
		SetOrderView();
        TimerAtZero();

    }

	public void DecrementSecondsBy(float seconds)
	{
		m_SecondsForComplete -= seconds;

	}

	public void SetFood(Food food)
	{
        this.m_Food = food;
		m_SecondsForComplete = food.GetSecondsToComplete();

		m_IsEmpty = false;
		SetOrderEmptyView();
	}

	void SetOrderView()
	{
		GetComponent<Text>().text = (int)m_SecondsForComplete + "\n" + m_Food.GetFoodName();
	}

	void SetOrderEmptyView() {
		GetComponent<Text>().text = "Empty";
	}
    #endregion

    #region timerLogic
    void ResetTimer()
    {
        m_SecondsForComplete = this.m_Food.GetSecondsToComplete();
    }

    void TimerAtZero()
    {
        if (m_SecondsForComplete <= 0)
        {
            ScoreManager.instance.DecrementScore();
            TimeManager.instance.DecrementGameTime();
            ResetTimer();
        }
    }
    #endregion

}
