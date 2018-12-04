using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPanel : InstructionsPanel {

    private void Awake()
    {
        base.completeButton.onClick.AddListener(delegate {
            StateManager.instance.startGame();
            vanishPanel();
        });
    }

    void vanishPanel() {
        iTween.ScaleTo(gameObject, Vector3.zero, 0.5f);
    }
}
