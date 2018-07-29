using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IngredientGenerator))]
public class CenterIngredient : MonoBehaviour
{
    [SerializeField]
    float m_TimeToReachTarget;
    [SerializeField]
    GameObject UserInput;

    float m_Speed;
    Vector3 m_StartPosition;
    Vector3 m_EndPosition;

    bool m_StartCenterIngredientAnimation;

    [SerializeField]
    GameObject m_IngredientGeneratorObj;
    IngredientGenerator m_IngredientGenerator;

    Direction m_SwipedDirection;

    #region Mono
    // Use this for initialization
    void Awake()
    {
        m_StartPosition = transform.position;
        m_StartCenterIngredientAnimation = false;
        m_IngredientGenerator = m_IngredientGeneratorObj.GetComponent<IngredientGenerator>();
        SetupDelegate();
    }

    void SetupDelegate()
    {
        UserInput.GetComponent<UserInput>().snapOffDelegate += StartSnapOffScreenAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        SnapOffScreenAnimation();
    }

    #endregion

    #region GetterSetter
    void StartSnapOffScreenAnimation(Direction dir)
    {
        this.m_SwipedDirection = dir;
        m_EndPosition = GetIngredient().transform.position;
        m_StartCenterIngredientAnimation = true;
    }

    Ingredient GetIngredient()
    {
        FoodHolder holder = m_IngredientGenerator.GetFoodHolder()[(int)m_SwipedDirection];
        return holder.GetStoredFood().GetNeededIngredient();
    }

    public float TimeToReachTarget()
    {
        return m_TimeToReachTarget;
    }

    public void SetCenter(Ingredient ingredient)
    {
        GetComponent<Image>().sprite = ingredient.CenterSpriteImage();
    }
    #endregion

    #region Movement

    void SnapOffScreenAnimation()
    {
        if (m_StartCenterIngredientAnimation)
        {
            m_Speed += Time.deltaTime / m_TimeToReachTarget;
            transform.position = Vector3.Lerp(transform.position, m_EndPosition, m_Speed);
            if (transform.position == m_EndPosition)
            {
                ResetCenterAnimation();
                transform.position = m_StartPosition;
                UserInput.GetComponent<UserInput>().RunSwipeDelegate();
            }
        } 
    }

    void ResetCenterAnimation()
    {
        m_StartCenterIngredientAnimation = false;
        m_Speed = 0;
    }
    
    #endregion

}
