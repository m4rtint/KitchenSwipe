using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    //Delegate
    public delegate void TimerDelegate();
    public TimerDelegate updateTimerUIDelegate;
    public TimerDelegate isGameOverDelegate;

    [SerializeField]
    float gameTime;
    float redPenaltyTime;
    readonly float penaltySpeedUp = 10;
   
	[SerializeField]
    float foodPenaltyTime;

    [SerializeField]
    float gameTimePenaltyTime;

    [SerializeField]
    float foodSuccessPrizeTime;

	//Public variable
    public static TimeManager instance = null;
	public float orderTimeVaryingSpeed;

    #region Getter/Setter
	public float FoodPenaltyTime() {
		return foodPenaltyTime;
	}

    public float GameTime()
    {
        return gameTime;
    }

	public float RedTime()
	{
		return redPenaltyTime;
	}
    #endregion

    #region Mono
    void Awake()
    {
        instance = this;
		redPenaltyTime = gameTime;
    }
    void Update()
    {
		if (StateManager.instance.isInGame()) {
			decrementGameTime (Time.deltaTime);
			decrementRedTime (Time.deltaTime);
		}
    }

    #endregion
    
    #region Timing
    void decrementRedTime(float seconds) {
		if (redPenaltyTime > gameTime) {
			seconds *= penaltySpeedUp;
		} else if (redPenaltyTime < gameTime) {
			redPenaltyTime = gameTime;
		}
		redPenaltyTime -= seconds;

	}


    void decrementGameTime(float seconds)
    {
        if (gameTime - seconds <= 0) {
            gameTime = 0;
            StateManager.instance.gameOver();
            this.isGameOverDelegate();
			//TODO - This should call delegate and engine should handle everything
			ScoreManager.instance.saveScore ();
        }
        gameTime -= seconds;

        this.updateTimerUIDelegate();
    }

    public void penaltyGameTime()
    {
        gameTime -= gameTimePenaltyTime;

        this.updateTimerUIDelegate();
    }

    public void incrementGameTime(float time = 0)
    {
        if (time <= 0)
        {
            time = foodSuccessPrizeTime;
        }
        gameTime += time;
        this.updateTimerUIDelegate();
    }

    #endregion


}
