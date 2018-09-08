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

    //Highscore time
    float secondsLasted = 0;

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

    public int SecondsLasted()
    {
        return (int)secondsLasted;
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
            incrementHighScoreGameTime(Time.deltaTime);

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

    void incrementHighScoreGameTime(float seconds)
    {
        secondsLasted += seconds;
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
