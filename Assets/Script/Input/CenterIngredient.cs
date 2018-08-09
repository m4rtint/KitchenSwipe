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

    //Movement
    float m_MovementSpeed;
    Vector3 m_StartPosition;
    Vector3 m_EndPosition;

    //Rotation
    [SerializeField]
    float m_RotationSpeed;
    bool m_isRotateClockwise;

    bool m_StartCenterIngredientAnimation;
    bool m_StartCenterRotatationAnimation;
    Ingredient m_CenterIngredient;

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
        m_StartCenterRotatationAnimation = false;
        m_isRotateClockwise = true;
        m_IngredientGenerator = m_IngredientGeneratorObj.GetComponent<IngredientGenerator>();
        SetupDelegate();
    }

    void SetupDelegate()
    {
        UserInput.GetComponent<UserInput>().snapOffDelegate += StartCenterAnimation;
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGhostAnimation();
        RotateCenterAnimation();
    }

    #endregion

    #region GetterSetter
    Vector3 GetIngredientAtSwipedDirectionPosition()
    {
        FoodHolder holder = m_IngredientGenerator.GetFoodHolder()[(int)m_SwipedDirection];
        if (holder.GetStoredFood() != null)
        {
            return holder.GetStoredFood().GetNeededIngredient().transform.position;
        } else
        {
            return holder.transform.position;
        }
    }

    public float TimeToReachTarget()
    {
        return m_TimeToReachTarget;
    }

    public void SetCenter(Ingredient ingredient)
    {
        GetComponent<Image>().sprite = ingredient.CenterSpriteImage();
        m_CenterIngredient = ingredient;
    }
    #endregion

    #region Movement
    void StartCenterAnimation(Direction dir)
    {
        this.m_SwipedDirection = dir;
        m_EndPosition = GetIngredientAtSwipedDirectionPosition();
        if (m_CenterIngredient.GetType() == typeof(Sauce))
        {
            m_StartCenterRotatationAnimation = true;
            if (dir == Direction.Left)
            {
                m_isRotateClockwise = false;
            }
        }
        else
        {
            m_StartCenterIngredientAnimation = true;
        }

    }

    void SnapToGhostAnimation()
    {
        if (m_StartCenterIngredientAnimation)
        {
            m_MovementSpeed += Time.deltaTime / m_TimeToReachTarget;
            transform.position = Vector3.Lerp(transform.position, m_EndPosition, m_MovementSpeed);
            if (transform.position == m_EndPosition)
            {
                ResetCenterAnimation();
                UserInput.GetComponent<UserInput>().RunSwipeDelegate();
            }
        } 
    }

    void RotateCenterAnimation()
    {
        if (m_StartCenterRotatationAnimation)
        {
            if (!IsAngleReached())
                RotateCenter();
            else { 
                ResetCenterAnimation();
                UserInput.GetComponent<UserInput>().RunSwipeDelegate();
            }

        }
    }

    void RotateCenter()
    {
       float speed = m_RotationSpeed * Time.deltaTime;
      if (m_isRotateClockwise)
         transform.Rotate(Vector3.back * speed);
      else
         transform.Rotate(Vector3.forward * speed);
    }

    bool IsAngleReached()
    {
        switch (this.m_SwipedDirection)
        {
            case Direction.Up:
                return true;
            case Direction.Left:
                return transform.rotation.eulerAngles.z > 90;
            case Direction.Right:
                return transform.rotation.eulerAngles.z < 270 && transform.rotation.eulerAngles.z > 0;
            case Direction.Down:
                return transform.rotation.eulerAngles.z < 180 && transform.rotation.eulerAngles.z > 0;
            default:
                return false;
        }
    }

    void ResetCenterAnimation()
    {
        transform.position = m_StartPosition;
        transform.rotation = Quaternion.identity;
        m_StartCenterIngredientAnimation = false;
        m_StartCenterRotatationAnimation = false;
        m_isRotateClockwise = true;
        m_MovementSpeed = 0;
    }
    
    #endregion

}
