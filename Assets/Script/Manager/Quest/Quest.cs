﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest {

    protected Missions[] missions;

    protected int missionsPosition;
    QuestType type;
    string questText;

    protected Quest(QuestType type, string questString)
    {
        setMissions();
        generateQuest();
        this.type = type;
        this.questText = questString;
    }
    
    public QuestType Type()
    {
        return type;
    }

    protected virtual void setMissions()
    {
        missions = new Missions[]
        {
            new Missions(10, 500),
            new Missions(20, 1500),
            new Missions(30, 2500),
            new Missions(40, 3500),
            new Missions(50, 4500),
            new Missions(60, 5500),
        };
    }

    void generateQuest()
    {
        this.missionsPosition = Random.Range(0, missions.Length - 1);
    }

    protected void onQuestComplete()
    {
        ScoreManager.instance.incrementFinalScore(missions[missionsPosition].points);
    }

    protected bool checkQuestWithPoints(int points)
    {
        bool isComplete = points == missions[missionsPosition].quest;
        if (isComplete)
        {
            onQuestComplete();
        }
        return isComplete;
    }

    public abstract bool isQuestComplete(object p);

}