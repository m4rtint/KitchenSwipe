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
    public string GameTime(int dec = 2)
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
    #endregion

    #region Mono
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
		if (StateManager.instance.InGame()) {
			DecrementGameTime ();
		}
    }

    #endregion
    
    #region Timing
    public void DecrementGameTime()
    {
        m_GameTime -= Time.deltaTime;
        if (m_GameTime <= 0) {
            m_GameTime = 0;
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
