using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuManager : MonoBehaviour {

    [SerializeField]
    Button menuButton;
    [SerializeField]
    TextMeshProUGUI questsText;

    private void Awake()
    {
        menuButton.onClick.AddListener(() => TransitionManager.instance.startMainMenuScene());
        questsText.text = QuestManager.instance.getListOfQuestText();
    }

}
