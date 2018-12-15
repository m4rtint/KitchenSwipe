﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuManager : MonoBehaviour {

    Queue<MenuQuestAnimation> listOfQuestsAnimation = new Queue<MenuQuestAnimation>();
    Queue<int> indexOfQuest = new Queue<int>();

    [SerializeField]
    Button menuButton;
    [SerializeField]
    MenuQuestAnimation[] questsText;

    [SerializeField]
    TextMeshProUGUI completedQuestsText;

    [SerializeField]
    GameObject background;

    QuestManager questManager;

    private void Awake()
    {
        questManager = QuestManager.instance;
        menuButton.onClick.AddListener(() => TransitionManager.instance.startMainMenuScene());
        setupDelegates();
    }

    private void Start()
    {
        setupQuests();
    }

    public void setMenuOpen(bool isOpen)
    {
        Vector3 size = isOpen ? Vector3.one : Vector3.zero;

        Hashtable ht = new Hashtable();
        ht.Add("time", AnimationManager.instance.PauseMenuOpenTime());
        ht.Add("scale", size);
        if (isOpen) {
            ht.Add("oncomplete", "checkOffQuestsCompletedIfNeeded");
        }
        iTween.ScaleTo(gameObject, ht);

        background.SetActive(isOpen);
    }

    void onMenuOpen()
    {
        checkOffQuestsCompletedIfNeeded();
    }

    void setupQuests()
    {
        Quest[] quest = questManager.Quests();
        for (int i = 0; i < questsText.Length; i++)
        {
            setQuestTextProperties(i);
        }
    }

    void setupDelegates()
    {
        foreach(MenuQuestAnimation quest in questsText)
        {
            quest.onAnimationCompleteDelegate += onCompleteQuestAnimation;
        }
    }

    private void setQuestTextProperties(int index)
    {
        Quest quest = questManager.Quests()[index];
        questsText[index].setQuestText(quest.GetQuestText());
        questsText[index].setCheckMarkAppear(false);
        questsText[index].questId = quest.QuestID();
    }
    
    private void checkOffQuestsCompletedIfNeeded()
    {
        Quest[] listOfQuests = questManager.Quests();
        for(int i = 0; i < questsText.Length; i++)
        {
            if (listOfQuests[i].QuestID() != questsText[i].questId)
            {
                listOfQuestsAnimation.Enqueue(questsText[i]);
                indexOfQuest.Enqueue(i);
            }
        }

        startQuestAnimationIfneeded();
    }

    void startQuestAnimationIfneeded()
    {
        if (listOfQuestsAnimation.Count > 0)
        {
            MenuQuestAnimation questAnim = listOfQuestsAnimation.Dequeue();
            questAnim.questCompleteAnimation();
        }
    }

    void onCompleteQuestAnimation()
    {
        startQuestAnimationIfneeded();
        setQuestTextProperties(indexOfQuest.Dequeue());
    }
}
