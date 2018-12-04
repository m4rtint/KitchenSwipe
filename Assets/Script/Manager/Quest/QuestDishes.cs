using System.Collections;
using System.Collections.Generic;

public class QuestDishes : Quest {

    int currentDishes;

    public QuestDishes():base(QuestType.DISHES, QuestModel.DISH_QUEST){}

    public override bool isQuestComplete(object p)
    {
        currentDishes++;
        return checkQuestWithPoints(currentDishes);
    }

    protected override void setMissions()
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

    public override string getQuestText()
    {
        return string.Format(this.questText, missions[missionsPosition].quest);
    }
}
