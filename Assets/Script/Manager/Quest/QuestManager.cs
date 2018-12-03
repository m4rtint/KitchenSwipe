using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    [SerializeField]
    bool refreshQuests;

    FoodName[] listOfFood = null;

    Quest[] quests = new Quest[3];
    readonly int differentQuests = 3;

    public static QuestManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    public void setupQuestManager(Food[] foods)
    {
        listOfFood = new FoodName[foods.Length];
        for(int i = 0; i < foods.Length; i++)
        {
            listOfFood[i] = foods[i].getName();
        }

        for (int i = 0; i < listOfFood.Length-1; i++)
        {
            generateNewQuest(i);
        }
    }

    #region QuestGeneration
    public void generateNewQuest(int index)
    {
        if (listOfFood == null)
        {
            Debug.LogWarning("MUST PASS IN LIST OF FOOD BEFORE GENERATING ANYTHING");
        }

        QuestType type = (QuestType) Random.Range(0, differentQuests - 1);
        generateQuest(index, type);
    } 


    void generateQuest(int index, QuestType type)
    {
        if (index < quests.Length) { 
            switch(type)
            {
                case QuestType.COMBO:
                    quests[index] = new QuestCombo();
                    break;
                case QuestType.DISHES:
                    quests[index] = new QuestDishes();
                    break;
                case QuestType.FOOD:
                    quests[index] = new QuestFood(listOfFood);
                    break;
                case QuestType.POINTS:
                    break;
                case QuestType.TIME:
                    break;
            }
        }
    }
    #endregion

    #region QuestChecking

    public void checkCombo(int combo)
    {
        checkQuestComplete(combo, QuestType.COMBO);
    }

    public void checkPoints(int points)
    {
        checkQuestComplete(points, QuestType.POINTS);
    }

    public void checkTimer(float time)
    {
        checkQuestComplete(time, QuestType.TIME);
    }

    public void checkDishes(int dish)
    {
        checkQuestComplete(dish, QuestType.DISHES);
    }

    public void checkFoodCompleted(FoodName name)
    {
        checkQuestComplete(name, QuestType.FOOD);
    }

    void checkQuestComplete(object obj, QuestType type)
    {
        if (quests != null)
        {
            for (int i = 0; i < quests.Length; i++)
            {
                if (quests[i].Type() == type)
                {
                    if (quests[i].isQuestComplete(obj))
                    {
                        generateNewQuest(i);
                    }
                }
            }
        }
    }

    void onCompleteQuest(int index)
    {
        if (refreshQuests) { 
            generateNewQuest(index);
        }
    }
    #endregion
}
