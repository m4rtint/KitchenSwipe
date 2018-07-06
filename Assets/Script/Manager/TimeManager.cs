using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    //Delegate
    public delegate void TimerDelegate();
    public TimerDelegate thisDelegate;

    [SerializeField]
    float m_GameTime;
   
	[SerializeField]
	float m_OrderPenaltyTime;

    [SerializeField]
    float m_OrderSuccessGivenTime;
    public static TimeManager instance = null;

    #region Getter/Setter
    public string GameTime(int dec = 2)
    {
        return m_GameTime.ToString("n" + dec);
    }

	public float OrderPenaltyTime() {
		return m_OrderPenaltyTime;
	}
    #endregion

    #region Mono
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        DecrementGameTime();
    }

    #endregion
    
    #region Timing
    public void DecrementGameTime()
    {
        m_GameTime -= Time.deltaTime;
        this.thisDelegate();
    }

    public void IncrementGameTime(float time = 0)
    {
        if (time <= 0)
        {
            time = m_OrderSuccessGivenTime;
        }
        m_GameTime += time;
        this.thisDelegate();
    }

    #endregion


}
