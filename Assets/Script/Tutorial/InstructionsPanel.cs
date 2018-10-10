using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InstructionsPanel : MonoBehaviour {
    [SerializeField]
    TextMeshProUGUI instructions;

    [SerializeField]
    protected Button completeButton;

    protected virtual void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    public void setInstructions(string text) {
        instructions.text = text;
    }

    public void startPopoutAnimation()
    {
        iTween.ScaleTo(gameObject, Vector3.one, 1f);
    }
}
