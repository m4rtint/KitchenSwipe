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


    #region mono
    private void Awake()
    {
        foodHolder = GetComponent<FoodHolder>();
    }

    private void Start()
    {
        resetFoodTimerIfNeeded();
    }
    #endregion

    #region InitFoodTimer
    public void resetFoodTimerIfNeeded()
    {
        if (foodHolder.StoredFood() != null)
        {
            greenSecondsToComplete = foodHolder.StoredFood().SecondsToComplete();
            redSecondsToComplete = greenSecondsToComplete;
        }
    }
    #endregion


    float FoodTime()
    {
        Food food = foodHolder.StoredFood();
        if (food == null)
        {
            return -1;
        } else
        {
            return food.SecondsToComplete();
        }
    }

    public void decrementTimeBy(float seconds)
    {
        greenSecondsToComplete -= seconds;
    }

    void decrementRedTimeBy(float seconds)
    {
        if (redSecondsToComplete > greenSecondsToComplete)
        {
            redSecondsToComplete -= seconds*speedUp;
        } else if (redSecondsToComplete < greenSecondsToComplete)
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
    

    #region view
    public void updateTimer(float seconds)
    {
        if (foodHolder.StoredFood() != null) {
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
            barImage(greenBar);
        } else if(ratio > 0.2)
        {
            barImage(yellowBar);
        } else
        {
            barImage(redBar);
        }
    }

    void timerAtZero()
    {
        if (greenSecondsToComplete <= 0)
        {
            ScoreManager.instance.decrementScore();
            TimeManager.instance.penaltyGameTime();
            moveFoodToTrashIfneeded();
            foodTimerRanOutDelegate();

            //TODO - Get new food
            //Destroy current one
        }
    }

    void moveFoodToTrashIfneeded()
    {
        if (foodHolder.StoredFood() != null)
        {
            FoodAnimation foodAnim = foodHolder.StoredFood().Animation();
            Vector3 position = AnimationManager.instance.TrashPosition() - transform.localPosition;
            foodAnim.moveToTrash(position);
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
