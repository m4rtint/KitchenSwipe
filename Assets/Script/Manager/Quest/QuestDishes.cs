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
            new Missions(1, 500),
            new Missions(2, 1500),
            new Missions(3, 2500),
            new Missions(1, 3500),
            new Missions(1, 4500),
            new Missions(1, 5500),
        };
    }

    public override string getQuestText()
    {
        return string.Format(this.questText, missions[missionsPosition].quest);
    }
}
