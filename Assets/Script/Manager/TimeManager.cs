using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    //Delegate
    public delegate void TimerDelegate();
    public TimerDelegate UpdateTimerUIDelegate;
    public TimerDelegate CheckGameOverDelegate;

    [SerializeField]
    float m_GameTime;
	float m_RedPenaltyTime;
	readonly float m_PenaltySpeedUp = 10;
   
	[SerializeField]
	float m_FoodPenaltyTime;

    [SerializeField]
    float m_GameTimePenaltyTime;

    [SerializeField]
    float m_FoodSuccessPrizeTime;

	//Public variable
    public static TimeManager instance = null;
	public float m_OrderTimeVaryingSpeed;

    #region Getter/Setter
    public string GameTime(int dec = 0)
    {
        return m_GameTime.ToString("n" + dec);
    }

	public float OrderPenaltyTime() {
		return m_FoodPenaltyTime;
	}

    public float GameTimeF()
    {
        return m_GameTime;
    }

	public float RedTimeF()
	{
		return m_RedPenaltyTime;
	}
    #endregion

    #region Mono
    void Awake()
    {
        instance = this;
		m_RedPenaltyTime = m_GameTime;
    }
    void Update()
    {
		if (StateManager.instance.InGame()) {
			DecrementGameTime (Time.deltaTime);
			DecrementRedTime (Time.deltaTime);
		}
    }

    #endregion
    
    #region Timing
	void DecrementRedTime(float seconds) {
		if (m_RedPenaltyTime > m_GameTime) {
			seconds *= m_PenaltySpeedUp;
		} else if (m_RedPenaltyTime < m_GameTime) {
			m_RedPenaltyTime = m_GameTime;
		}
		m_RedPenaltyTime -= seconds;

	}


	void DecrementGameTime(float seconds)
    {
		m_GameTime -= seconds;
        if (m_GameTime <= 0) {
            m_GameTime = 0;
            StateManager.instance.GameOver();
            this.CheckGameOverDelegate();
        }
        this.UpdateTimerUIDelegate();
    }

    public void PenaltyGameTime()
    {
        m_GameTime -= m_GameTimePenaltyTime;

        this.UpdateTimerUIDelegate();
    }

    public void IncrementGameTime(float time = 0)
    {
        if (time <= 0)
        {
            time = m_FoodSuccessPrizeTime;
        }
        m_GameTime += time;
        this.UpdateTimerUIDelegate();
    }

    #endregion


}
