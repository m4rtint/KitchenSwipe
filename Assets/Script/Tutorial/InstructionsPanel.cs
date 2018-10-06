using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InstructionsPanel : MonoBehaviour {
    [SerializeField]
    TextMeshProUGUI instructions;

    [SerializeField]
    Button completeButton;

    private void Awake()
    {
        completeButton.onClick.AddListener(delegate {
            TransitionManager.instance.startMainMenuScene();
        });
        setCompletionButtonActive(false);
    }

    private void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
        setInstructions(InstructionsMessages.TUTORIAL_STEP_ONE);
    }

    public void setInstructions(string text) {
        instructions.text = text;
    }

    public void startPopoutAnimation() {
        iTween.ScaleTo(gameObject, Vector3.one, 1f);
    }

    public void setCompletionButtonActive(bool set){
        completeButton.gameObject.SetActive(set);
    }
}
