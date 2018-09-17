using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
    [Header("Ingredient Placement")]
    [SerializeField]
    float placementTime;
    [SerializeField]
    float neededIngredientAlpha;

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

    [Header("Center")]
    [SerializeField]
    float rotationTime;
    [SerializeField]
    float moveTime;

    [Header("Sauce Fadeout Time")]
    [SerializeField]
    float centerFadeOutTime;

    [Header("Combo Pop Time")]
    [SerializeField]
    float comboPopTime;

    [Header("Score Pop")]
    [SerializeField]
    float scoreDelayTime;
    [SerializeField]
    float scorePopTime;

    [Header("GameOver Screen")]
    [SerializeField]
    float goFadeInTime;
    
    public static AnimationManager instance = null;

    #region mono
    void Awake()
    {
        instance = this;
        SetupAscensionFadedIfNeeded();
    }

    void SetupAscensionFadedIfNeeded()
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

    public float ComboPopTime()
    {
        return comboPopTime;
    }

    public float ScoreDelayTime()
    {
        return scoreDelayTime;
    }

    public float ScorePopTime()
    {
        return scorePopTime;
    }

    public float GameOverFadeInTime()
    {
        return goFadeInTime;
    }

    public float FoodShakeTime()
    {
        return foodShakeTime;
    }

    public float FoodShakeAmount()
    {
        return foodShakeAmount;
    }

    public float NeededIngredientAlpha()
    {
        return neededIngredientAlpha;
    }
    #endregion
}
