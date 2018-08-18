using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    [Header("Center Movement")]
    [SerializeField]
    float m_MoveTime;

    [Header("Ingredient Placement")]
    [SerializeField]
    float m_PlacementTime;

    [Header("Complete Food Ascension")]
    [SerializeField]
    float m_AscensionTime;
    [SerializeField]
    float m_AscensionAmount;
    [SerializeField]
    float m_AscensionFadeTime;

    [Header("Center Rotation Time")]
    [SerializeField]
    float m_RotationTime;

    [Header("Sauce Fadeout Time")]
    [SerializeField]
    float m_CenterFadeOutTime;

    [Header("Combo Pop Time")]
    [SerializeField]
    float m_ComboPopTime;

    [Header("Score Rise")]
    [SerializeField]
    float m_ScoreRiseTime;
    [SerializeField]
    float m_ScoreRiseAmount;

    public static AnimationManager instance = null;

    #region mono
    void Awake()
    {
        instance = this;
        SetupAscensionFadedIfNeeded();
    }

    void SetupAscensionFadedIfNeeded()
    {
        if (m_AscensionFadeTime > m_AscensionTime) { m_AscensionFadeTime = m_AscensionTime / 2; }

    }
    #endregion

    #region Getter
    public float CenterMoveTime()
    {
        return m_MoveTime;
    }

    public float PlacementTime()
    {
        return m_PlacementTime;
    }

    public float AscensionTime()
    {
        return m_AscensionTime;
    }

    public float AscensionAmount()
    {
        return m_AscensionAmount;
    }

    public float AscensionFade()
    {
        return m_AscensionFadeTime;
    }

    public float RotationTime()
    {
        return m_RotationTime;
    }

    public float SauceFadeOutTime()
    {
        return m_CenterFadeOutTime;
    }

    public float ComboPopTime()
    {
        return m_ComboPopTime;
    }

    public float ScoreRiseTime()
    {
        return m_ScoreRiseTime;
    }

    public float ScoreRiseAmount()
    {
        return m_ScoreRiseAmount;
    }

    #endregion
}
