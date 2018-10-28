using System.Collections;
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
    [SerializeField]
    float comboPopScale;

    [Header("Score Pop")]
    [SerializeField]
    float scoreDelayTime;
    [SerializeField]
    float scorePopTime;

    [Header("GameOver Screen")]
    [SerializeField]
    float goFadeInTime;

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
        float mt = moveTime - 0.04f;
        float pt = placementTime - 0.03f;

        rotationTime = Mathf.Max(rt, 0.1f);
        moveTime = Mathf.Max(mt, 0.1f);
        placementTime = Mathf.Max(pt, 0.1f);
    }

    public void setTrashPosition(Vector3 pos)
    {
        if (trashPosition == Vector3.zero) { 
            trashPosition = pos;
        }
    }
    #endregion
}
