﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(ScoreManager))]
public class GameEngineInfinite : GameEngine
{

    [SerializeField]
    int m_WaveSize;

    int numberOfCompletedOrder;

    //Announcement Manager
    [SerializeField]
    AnnouncementManager announcementManager;

    [SerializeField]
    GoalPanel goalManager;

    #region Mono
    protected override void Start()
    {
        if (base.NumberOfFood() < m_WaveSize)
        {
            base.NumberOfFood(m_WaveSize);
        }
        base.Start();
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
        if (numberOfCompletedOrder % m_WaveSize == 0)
        {
            IncrementDifficulty();
        }
        if (numberOfCompletedOrder == 50)
        {
            m_WaveSize = 15;
        }
    }


    void IncrementDifficulty()
    {
        //Order Timers go down faster
        float speedUpdate = 0;
        float scoreUpdate = 0;
        // - Goes up 
        if (numberOfCompletedOrder <= m_WaveSize * 6)
        {
            speedUpdate = 0.5f;
            scoreUpdate = 0.5f;
        }
        else if (numberOfCompletedOrder <= m_WaveSize * 11)
        {
            speedUpdate = 0.2f;
        }
        if (numberOfCompletedOrder > m_WaveSize * 6 && numberOfCompletedOrder <= m_WaveSize * 9)
        {
            scoreUpdate = 2f;
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
