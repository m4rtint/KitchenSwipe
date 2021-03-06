﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFood : Quest
{ 
    Food chosenFood;
    int numOfCompletedFood;

    FoodName getFoodEnum()
    {
        return chosenFood.getEnumName();
    }

    string getFoodName()
    {
        return chosenFood.getFoodName();
    }

    public QuestFood(Food[] names) : base(QuestType.FOOD, QuestModel.FOOD_QUEST) {
        int index = Random.Range(0, names.Length - 1);
        chosenFood = names[index];
    }    

    public override bool isQuestComplete(object p)
    {
        FoodName name = (FoodName)p;
        if (name == getFoodEnum())
        {
            numOfCompletedFood++;
        }

        return checkQuestWithPoints(numOfCompletedFood);
    }

    protected override void setMissions()
    {
        missions = new Missions[]
        {
            new Missions(2, 300),
            new Missions(3, 900),
            new Missions(4, 1000),
            new Missions(5, 3000),
            new Missions(7, 10000)
        };
    }

    public override string getQuestText()
    {
        return string.Format(this.questText, missions[missionsPosition].quest, getFoodName());
    }

    public override int getCurrentMissionValue()
    {
        return numOfCompletedFood;
    }
}
