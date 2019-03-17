using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedQuestViewManager : MonoBehaviour {

    private Text listOfCompletedQuestText;
    private RectTransform rectTransform;
    private QuestManager questManager;


    const float characterToSecondLine = 36;
    const float singleLineHeight = 80f;
    const float twoLineHeight = 120f;

    float heightOfPanel = 0;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        listOfCompletedQuestText = GetComponent<Text>();
        questManager = QuestManager.instance;
    }
    
    private void OnEnable()
    {
        fillQuestList();
        setQuestViewerHeight();
    }

    void fillQuestList()
    {
        if (completedQuests().Count > 0)
        {
            listOfCompletedQuestText.alignment = TextAnchor.UpperLeft;
            listOfCompletedQuestText.text = generateQuestList();
        }
    }

    void setQuestViewerHeight()
    {
        float bottom = -heightOfPanel;
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, bottom);
    }

    string generateQuestList()
    {
        ArrayList completed = completedQuests();
        string list = null;
        if (completed != null)
        {
            for (int i = 0; i < completed.Count; i++)
            {
                string quest = (string)completed[i];
                list += string.Format("{0}. {1}\n\n", i + 1, quest);

                heightOfPanel += (quest.Length > characterToSecondLine) ? twoLineHeight : singleLineHeight;
            }
        }

        return list;
    }

    ArrayList completedQuests()
    {
        return questManager.completedQuests;
    }
}
