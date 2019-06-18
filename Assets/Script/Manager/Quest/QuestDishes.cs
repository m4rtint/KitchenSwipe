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
            new Missions(3, 300),
            new Missions(7, 500),
            new Missions(8, 700),
            new Missions(10, 1000),
            new Missions(12, 1500),
            new Missions(15, 1700),
        };
    }

    public override string getQuestText()
    {
        return string.Format(this.questText, missions[missionsPosition].quest);
    }

    public override int getCurrentMissionValue()
    {
        return currentDishes;
    }
}
