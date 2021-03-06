﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
    [Header("Ingredient Placement")]
    [SerializeField]
    float placementTime;
    [SerializeField]
    float ingredientAlpha;

    [Header("Complete Food Ascension")]
    [SerializeField]
    float ascensionTime;
    [SerializeField]
    float ascensionAmount;
    [SerializeField]
    float ascentionFadeTime;

    [Header("Incorrect Food")]
    [SerializeField]
    float foodShakeTime;
    [SerializeField]
    float foodShakeAmount;
    [SerializeField]
    float moveToTrashTime;

    [Header("Center")]
    [SerializeField]
    float rotationTime;
    [SerializeField]
    float moveTime;

    [Header("Sauce Fadeout Time")]
    [SerializeField]
    float centerFadeOutTime;

    [Header("Quest Times")]
    [SerializeField]
    float questRiseUpTime;
    [SerializeField]
    float questDelayTime;

    [Header("Combo Pop Time")]
    [SerializeField]
    float comboPopTime;
    [SerializeField]
    float comboPopScale;

    [Header("Score Pop")]
    [SerializeField]
    float scoreDelayTime;
    [SerializeField]
    float scorePopTime;

    [Header("Penalty Time")]
    [SerializeField]
    float penaltyRiseAmount;
    [SerializeField]
    float penaltyRiseTime;

    [Header("GameOver Screen")]
    [SerializeField]
    float goFadeInTime;

    [Header("Pause Menu")]
    [SerializeField]
    float pauseMenuOpenTime;

    Vector3 trashPosition = Vector3.zero;
    public static AnimationManager instance = null;

    #region mono
    void Awake()
    {
        instance = this;
        setupAscensionFadedIfNeeded();
    }

    void setupAscensionFadedIfNeeded()
    {
        if (ascentionFadeTime > ascensionTime) { ascentionFadeTime = ascensionTime / 2; }
    }
    #endregion

    #region Getter
    public float CenterMoveTime()
    {
        return moveTime;
    }

    public float PlacementTime()
    {
        return placementTime;
    }

    public float AscensionTime()
    {
        return ascensionTime;
    }

    public float AscensionAmount()
    {
        return ascensionAmount;
    }

    public float AscensionFade()
    {
        return ascentionFadeTime;
    }

    public float RotationTime()
    {
        return rotationTime;
    }

    public float SauceFadeOutTime()
    {
        return centerFadeOutTime;
    }

    public float QuestRiseUpTime()
    {
        return questRiseUpTime;
    }

    public float QuestDelayTime()
    {
        return questDelayTime;
    }

    public float ComboPopTime()
    {
        return comboPopTime;
    }

    public Vector3 ComboPopScale()
    {
        return new Vector3(comboPopScale, comboPopScale, 0);
    }

    public float ScoreDelayTime()
    {
        return scoreDelayTime;
    }

    public float ScorePopTime()
    {
        return scorePopTime;
    }

    public float PenaltyRiseAmount()
    {
        return penaltyRiseAmount;
    }

    public float PenaltyRiseTime()
    {
        return penaltyRiseTime;
    }

    public float GameOverFadeInTime()
    {
        return goFadeInTime;
    }

    public float PauseMenuOpenTime()
    {
        return pauseMenuOpenTime;
    }

    public float FoodShakeTime()
    {
        return foodShakeTime;
    }

    public float FoodShakeAmount()
    {
        return foodShakeAmount;
    }

    public float MoveToTrashTime()
    {
        return moveToTrashTime;
    }

    public float NeededIngredientAlpha()
    {
        return ingredientAlpha;
    }

    public Vector3 TrashPosition()
    {
        return trashPosition;
    }

    #endregion

    #region Setter
    public void incrementSpeed()
    {
        float rt = rotationTime - 0.04f;
        float mt = moveTime - 0.02f;
        float pt = placementTime - 0.02f;

        rotationTime = Mathf.Max(rt, 0.2f);
        moveTime = Mathf.Max(mt, 0.2f);
        placementTime = Mathf.Max(pt, 0.2f);
    }

    public void setTrashPosition(Vector3 pos)
    {
        if (trashPosition == Vector3.zero) { 
            trashPosition = pos;
        }
    }
    #endregion
}
