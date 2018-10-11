using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPanel : InstructionsPanel {

    private void Awake()
    {
        setGoal();
        base.completeButton.onClick.AddListener(delegate {
            StateManager.instance.startGame();
            vanishPanel();
        });
    }

    void setGoal() {
        setInstructions("get as MANY points as possible before time runs out");
    }

    void vanishPanel() {
        iTween.ScaleTo(gameObject, Vector3.zero, 0.5f);
    }
}
