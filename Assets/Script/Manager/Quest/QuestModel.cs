using System.Collections;


public enum QuestType
{
    COMBO,
    DISHES,
    FOOD,
    TIME,
    POINTS,
}

public class QuestModel {
    public static string COMBO_QUEST = "Obtain a combo of {0}";
    public static string POINT_QUEST = "Obtain a total amount of {0} points";
    public static string DISH_QUEST = "Finish {0} amount of dishes";
    public static string FOOD_QUEST = "Complete {0} {1}s";
}


public class Missions
{
    public int quest;
    public int points;

    public Missions(int c, int p)
    {
        this.quest = c;
        this.points = p;
    }
}
