using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FoodHolder))]
public class FoodTimer : MonoBehaviour {

    //Delegate
    public delegate void TimerDelegate();
    public TimerDelegate foodTimerRanOutDelegate;

    //Dependencies
    FoodHolder foodHolder;

    [SerializeField]
    GameObject greenTimerObject;
    float greenSecondsToComplete;

    [SerializeField]
    GameObject redTimerObject;
    float redSecondsToComplete;
    readonly float speedUp = 10;

    [Header("Color Bars")]
    [SerializeField]
    Sprite greenBar;
    [SerializeField]
    Sprite yellowBar;
    [SerializeField]
    Sprite redBar;

    string shakeTween = null;
    bool isRunning = true;
    bool[] currColor = new bool[] { true, true, true };

    #region mono
    private void Awake()
    {
        foodHolder = GetComponent<FoodHolder>();
        shakeTween = "redShake" + foodHolder.Direction();
    }

    private void Start()
    {
        resetFoodTimerIfNeeded();
    }
    #endregion

    #region getter/setter
    public void run()
    {
        isRunning = true;
    }

    public void pause()
    {
        isRunning = false;
    }
    #endregion

    #region InitFoodTimer
    public void resetFoodTimerIfNeeded()
    {
        if (foodHolder.StoredFood() != null)
        {
            greenSecondsToComplete = foodHolder.StoredFood().SecondsToComplete();
            redSecondsToComplete = greenSecondsToComplete;
            run();
            currColor = new bool[] { true, true, true };
            stopBarShake();
        }
    }
    #endregion

    #region View
    public void decrementTimeBy(float seconds)
    {
        greenSecondsToComplete -= seconds;
    }

    public GameObject TimerObject()
    {
        return greenTimerObject.transform.parent.gameObject;
    }

    public void updateTimer(float seconds)
    {
        if (foodHolder.StoredFood() != null && isRunning) {
            decrementTimeBy(seconds);
            decrementRedTimeBy(seconds);
            updateTimerUI();
            updateTimerColor();
            timerAtZero();
        }
    }

    void updateTimerUI()
    {
        transformObjectXScale(greenTimerObject, calculateRatio(greenSecondsToComplete));
        transformObjectXScale(redTimerObject, calculateRatio(redSecondsToComplete));
    }

    void updateTimerColor()
    {
        float ratio = calculateRatio(greenSecondsToComplete);
        if (ratio > 0.4)
        {
            if (currColor[0]) { 
                barImage(greenBar);
                currColor[0] = false;
            }

        } else if(ratio > 0.25)
        {
            if (currColor[1]) { 
                barImage(yellowBar);
                currColor[1] = false;
                shakeBar(2f);
            }
        } else if (currColor[2])
        {
            barImage(redBar);
            currColor[2] = false;
            shakeBar(5f);
        }
    }

    void timerAtZero()
    {
        if (greenSecondsToComplete <= 0)
        {
            TimeManager.instance.penaltyGameTime();

            foodHolder.moveFoodToTrashIfneeded();
            foodTimerRanOutDelegate();
        }
    }
    #endregion

    #region shake
    void shakeBar(float amount)
    {
        Hashtable ht = new Hashtable();
        ht.Add("name", shakeTween);
        ht.Add("x", amount);
        ht.Add("looptype", "loop");
        ht.Add("time", 30f);
        iTween.ShakePosition(TimerObject(), ht);
    }

    void stopBarShake()
    {
        iTween.StopByName(shakeTween);
    }
    #endregion

    #region helper
    void decrementRedTimeBy(float seconds)
    {
        if (redSecondsToComplete > greenSecondsToComplete)
        {
            redSecondsToComplete -= seconds * speedUp;
        }
        else if (redSecondsToComplete < greenSecondsToComplete)
        {
            redSecondsToComplete = greenSecondsToComplete;
        }
        redSecondsToComplete -= seconds;
    }

    float calculateRatio(float seconds)
    {
        float ratio = 0;
        if (FoodTime() > 0 && greenSecondsToComplete >= 0)
        {
            ratio = seconds / FoodTime();
        }
        return ratio;
    }

    float FoodTime()
    {
        Food food = foodHolder.StoredFood();
        if (food == null)
        {
            return -1;
        }
        else
        {
            return food.SecondsToComplete();
        }
    }
    #endregion

    #region Tools
    void barImage(Sprite sprite)
    {
        greenTimerObject.GetComponent<Image>().sprite = sprite;
    }

    void transformObjectXScale(GameObject obj, float ratio)
    {
        obj.GetComponent<RectTransform>().localScale = new Vector3(ratio, 1, 1);
    }
    #endregion
}
