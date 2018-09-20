using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    [SerializeField]
    GameObject greenRingObject;
    Image greenRingImage;

	[SerializeField]
    GameObject redRingObject;
    Image redRingImage;

    [SerializeField]
    GameObject timeTextObject;
    Text timeText;

    //Dependencies
    TimeManager timeManager;

    float fullTime = -1;

    #region Mono
    private void Awake()
    {
        greenRingImage = greenRingObject.GetComponent<Image>();
		redRingImage = redRingObject.GetComponent<Image>();
        timeText = timeTextObject.GetComponent<Text>();
        timeManager = TimeManager.instance;
        initDelegate();
    }

    private void Start()
    {
        initTimer();
    }

    void initTimer()
    {
        if (fullTime <= -1)
        {
            fullTime = timeManager.GameTime();
        }
    }

    void initDelegate()
    {
        timeManager.updateTimerUIDelegate += updateText;
        timeManager.updateTimerUIDelegate += updateRing;
    }
    #endregion

    #region UI
    void updateText()
    {
        timeText.text = ((int)timeManager.GameTime()).ToString();
    }

    void updateRing()
    {
        if (timeManager.GameTime() > fullTime)
        {
            fullTime = timeManager.GameTime();
        }

		redRingImage.fillAmount = timeManager.RedTime () / fullTime;
        greenRingImage.fillAmount = timeManager.GameTime() / fullTime;
    }
    #endregion




}
