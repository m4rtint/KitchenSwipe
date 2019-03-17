using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(ScoreManager))]
public class GameEngineInfinite : GameEngine
{

    [Header("Infinite Local")]
    [SerializeField]
    int waveSize;
    int numberOfCompletedOrder;

    //Announcement Manager
    [Header("Infinite Dependencies")]
    [SerializeField]
    AnnouncementManager announcementManager;
    [SerializeField]
    GoalPanel goalManager;

    #region Mono
    protected override void Start()
    {
        if (base.NumberOfFood() < waveSize)
        {
            base.NumberOfFood(waveSize);
        }
        base.Start();
        questManager.setupQuestManager(foodGenerator.Foods());
        goalManager.setInstructions(questManager.getListOfQuestText());
    }

    protected override void setupDelegates()
    {
        base.setupDelegates();
        announcementManager.onTimesUpComplete += GetComponent<UIManager>().startGameOverScreen;
        TransitionManager.instance.completedTransition += onCompleteTransition; 
    }

    #endregion
    protected override void CompleteOrder(Direction food)
    {
        numberOfCompletedOrder++;
      
        base.CompleteOrder(food);

        if (foodGenerator.ChosenFoodStack().Count < 8)
        {
            foodGenerator.fillStackWithRandomFood(base.NumberOfFood());
        }

        SetDifficultyIfNeeded();
    }

    void SetDifficultyIfNeeded()
    {
        if (numberOfCompletedOrder % waveSize == 0 && numberOfCompletedOrder > 0)
        {
            IncrementDifficulty();
        }
    }


    void IncrementDifficulty()
    {
        //Order Timers go down faster
        float speedUpdate = 0;
        float scoreUpdate = 0;
        // - Goes up 
        if (numberOfCompletedOrder <= waveSize * 6)
        {
            speedUpdate = 0.5f;
            scoreUpdate = 0.5f;
            AnimationManager.instance.incrementSpeed();
        }

        TimeManager.instance.orderTimeVaryingSpeed += speedUpdate;
        ScoreManager.instance.addToScoreMultiplier(scoreUpdate);
    }

    #region delegate
    protected override void onGameOver()
    {
        base.onGameOver();
        announcementManager.gameObject.SetActive(true);
        announcementManager.startTimesUpAnimate();
    }

    void onCompleteTransition()
    {
        //DISPLAY INSTRUCTIONS
        goalManager.startPopoutAnimation();
    }
    #endregion

}
