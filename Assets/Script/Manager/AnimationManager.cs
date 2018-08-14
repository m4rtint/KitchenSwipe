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

    [Header("Center Rotation Time")]
    [SerializeField]
    float m_RotationTime;

    [Header("Sauce Fadeout Time")]
    [SerializeField]
    float m_CenterFadeOutTime;

    public static AnimationManager instance = null;

#region mono
    void Awake()
    {
        instance = this;
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

    public float RotationTime()
    {
        return m_RotationTime;
    }

    public float SauceFadeOutTime()
    {
        return m_CenterFadeOutTime;
    }
    #endregion
}
