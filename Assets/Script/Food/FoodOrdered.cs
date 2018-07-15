using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodOrdered : MonoBehaviour
{
    Food m_Food;
	float m_SecondsForComplete;

    //Children GameObjects
    [SerializeField]
    GameObject m_TimerBarObject;
    [SerializeField]
    GameObject m_FoodGameObject;

	#region Setter/Getter
	public float GetSecondsForComplete(){
		return m_SecondsForComplete;
	}

	public bool IsEmpty()
	{
		return m_Food == null;
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
	}

	public void UpdateOrderView(float seconds)
	{

		if (IsEmpty())
        {
            SetOrderEmptyView();
            return;
        }
		DecrementSecondsBy(seconds);
        UpdateOrderViewTimer();
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
        m_FoodGameObject.GetComponent<Image>().sprite = this.m_Food.GetImage();
	}

	void UpdateOrderViewTimer()
	{
        float ratio = m_SecondsForComplete / m_Food.GetSecondsToComplete();
        m_TimerBarObject.GetComponent<RectTransform>().localScale = new Vector3(ratio, 1, 1);
	}

	void SetOrderEmptyView() {
        m_FoodGameObject.GetComponent<Image>().sprite = null;
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
            TimeManager.instance.PenaltyGameTime();
            ResetTimer();
        }
    }
    #endregion

}
