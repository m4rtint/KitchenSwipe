using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    [Header("Settings")]
    [SerializeField]
    bool refreshQuests;
    [SerializeField]
    int activeAmountOfQuests = 3;

    [Header("Dependencies")]
    [SerializeField]
    Text mainGameQuestText;

    string mainQuests;
    Food[] listOfFood = null;

    Quest[] quests = new Quest[3];
    readonly int differentTypesOfQuests = 3;

    public static QuestManager instance = null;

    private void Awake()
    {
        instance = this;
        setInGameQuestText();
    }

    void setInGameQuestText(string text = "")
    {
        if (mainGameQuestText != null)
        {
            mainGameQuestText.text = text;
        }
    }

    public void setupQuestManager(Food[] foods)
    {
        this.listOfFood = foods;

        for (int i = 0; i < activeAmountOfQuests; i++)
        {
            generateNewQuest(i);
        }
    }
    #region Helper
    public string getListOfQuestText()
    {
        string listOfQuests = "";
        for(int i = 0; i < quests.Length; i++)
        {
            listOfQuests += string.Format("- {0}\n", quests[i].getQuestText());
        }
        return listOfQuests;
    }
    #endregion

    #region QuestGeneration
    public void generateNewQuest(int index)
    {
        if (listOfFood == null)
        {
            Debug.LogWarning("MUST PASS IN LIST OF FOOD BEFORE GENERATING ANYTHING");
        }

        QuestType type = (QuestType) Random.Range(0, differentTypesOfQuests);
        Debug.Log(type);
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
