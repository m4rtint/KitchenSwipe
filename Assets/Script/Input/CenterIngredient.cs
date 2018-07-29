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

    bool m_StartSnapOffScreenAnimation;

    [SerializeField]
    GameObject m_IngredientGeneratorObj;

    #region Mono
    // Use this for initialization
    void Awake()
    {
        m_StartPosition = transform.position;
        m_StartSnapOffScreenAnimation = false;
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
        m_EndPosition = GetIngredientPosition(dir);
        m_StartSnapOffScreenAnimation = true;
    }

    Vector3 GetIngredientPosition(Direction dir)
    {
        FoodHolder holder = m_IngredientGeneratorObj.GetComponent<IngredientGenerator>().GetFoodHolder()[(int)dir];
        GameObject ingredient = holder.GetStoredFood().GetNeededIngredient().gameObject;
        return ingredient.transform.position;
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
        if (m_StartSnapOffScreenAnimation)
        {
            m_Speed += Time.deltaTime / m_TimeToReachTarget;
            transform.position = Vector3.Lerp(transform.position, m_EndPosition, m_Speed);
            if (transform.position == m_EndPosition)
            {
                m_StartSnapOffScreenAnimation = false;
                transform.position = m_StartPosition;
                m_Speed = 0;
                UserInput.GetComponent<UserInput>().RunSwipeDelegate();
            }
        }
    }
    #endregion

}
