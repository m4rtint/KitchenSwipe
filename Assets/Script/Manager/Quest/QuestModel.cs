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
    public static string COMBO_QUEST = "Obtain a combo of %d";
    public static string POINT_QUEST = "Obtain a totall amount of %d points";
    public static string DISH_QUEST = "Finish %d amount of dishes";
    public static string FOOD_QUEST = "Complete %d %ss";
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
