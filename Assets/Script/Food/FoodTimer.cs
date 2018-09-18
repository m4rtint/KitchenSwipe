using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FoodHolder))]
public class FoodTimer : MonoBehaviour {

    Color hundredPercent;
    Color fourtyPercent;
    Color twentyPercent;

    //Dependencies
    FoodHolder foodHolder;

    [SerializeField]
    GameObject greenTimerObject;
    float greenSecondsToComplete;

    [SerializeField]
    GameObject redTimerObject;
    float redSecondsToComplete;
    readonly float speedUp = 10;



    #region mono
    private void Awake()
    {
        foodHolder = GetComponent<FoodHolder>();
        hundredPercent = convertColor(109, 255, 0);
        fourtyPercent = convertColor(250, 138, 2);
        twentyPercent = convertColor(255, 3, 0);
    }

    private void Start()
    {
        resetFoodTimerIfNeeded();
    }
    #endregion

    #region InitFoodTimer
    public void resetFoodTimerIfNeeded()
    {
        Food storedFood = foodHolder.StoredFood();
        if (storedFood != null)
        {
            greenSecondsToComplete = storedFood.SecondsToComplete();
            redSecondsToComplete = greenSecondsToComplete;
        }
    }
    #endregion

    #region gettersetter
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

    #endregion

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
        greenTimerObject.GetComponent<RectTransform>().localScale = new Vector3(calculateRatio(greenSecondsToComplete), 1, 1);
        redTimerObject.GetComponent<RectTransform>().localScale = new Vector3(calculateRatio(redSecondsToComplete), 1, 1);
    }

    void updateTimerColor()
    {
        float ratio = calculateRatio(greenSecondsToComplete);
        if (ratio > 0.4)
        {
            greenTimerObject.GetComponent<Image>().color = hundredPercent;
        } else if(ratio > 0.2)
        {
            greenTimerObject.GetComponent<Image>().color = fourtyPercent;
        } else
        {
            greenTimerObject.GetComponent<Image>().color = twentyPercent;
        }
    }

    void timerAtZero()
    {
        if (greenSecondsToComplete <= 0)
        {
            ScoreManager.instance.decrementScore();
            TimeManager.instance.penaltyGameTime();
            resetFoodTimerIfNeeded();
            shakeFoodIfNeeded();
        }
    }

    void shakeFoodIfNeeded() {
        FoodAnimation foodAnim = foodHolder.StoredFood().Animation();
        if (foodHolder.StoredFood() != null) {
            foodAnim.shakeFood();
            foodAnim.quicklyChangeFoodColor(Color.red);
        }
    }
    #endregion

    #region tools
    Color convertColor(float r, float g, float b)
    {
        float red = r / 255;
        float green = g / 255;
        float blue = b / 255;
        return new Color(red, green, blue);
    } 
    #endregion

}
