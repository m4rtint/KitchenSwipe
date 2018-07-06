using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FoodGenerator))]
[RequireComponent(typeof(TimeManager))]
[RequireComponent(typeof(ScoreManager))]
public class GameEngineInfinite : GameEngine {

    [SerializeField]
    int m_WaveSize;

    int m_NumberOfCompletedOrder;

    protected override void Start()
    {
        if (base.NumberOfFood() < m_WaveSize)
        {
            base.NumberOfFood(m_WaveSize);
        }
        base.Start();
    }

    public override void CompleteOrder(Food food)
    {
        m_NumberOfCompletedOrder++;

        base.CompleteOrder(food);

        if (m_FoodGenerator.GetChosenFoodStack().Count < 6) {
            m_FoodGenerator.FillStackWithRandomFood(base.NumberOfFood());
        }

        SetDifficultyIfNeeded();
    }

    void SetDifficultyIfNeeded(){
        if (m_NumberOfCompletedOrder % m_WaveSize == 0)
        {
            IncrementDifficulty();
        }
    }


    void IncrementDifficulty(){
        
    }
}
