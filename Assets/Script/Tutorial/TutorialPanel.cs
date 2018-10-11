using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : InstructionsPanel {


    private void Awake()
    {
        base.completeButton.onClick.AddListener(delegate {
            TransitionManager.instance.startMainMenuScene();
        });
        setCompletionButtonActive(false);
    }

    protected override void Start()
    {
        base.Start();
        setInstructions(InstructionsMessages.TUTORIAL_STEP_ONE);
    }

    public void setCompletionButtonActive(bool set)
    {
        completeButton.gameObject.SetActive(set);
    }
}
