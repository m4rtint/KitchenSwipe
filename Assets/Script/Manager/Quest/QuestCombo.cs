using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCombo : Quest
{
    int currentCombo;
    int missionCombo;

    #region abstract
    public QuestCombo() : base(QuestType.COMBO, QuestModel.COMBO_QUEST) { }


    public override bool isQuestComplete(object p)
    {
        int newCombo = (int)p;
        int diff = Mathf.Abs(newCombo - currentCombo);
        if (diff > 1)
        {
            missionCombo = 1;
        } else
        {
            missionCombo++;
        }
        currentCombo = newCombo;

       return  checkQuestWithPoints(missionCombo);
    }


    protected override void setMissions()
    {
        base.missions = new Missions[] {
            new Missions(5, 200),
            new Missions(10, 400),
            new Missions(15, 800),
            new Missions(20, 1600),
            new Missions(25, 3200),
            new Missions(30, 6400)
        };
    }
    #endregion

    #region helper
    void comboReset()
    {
        currentCombo = 0;
    }
    #endregion

}
