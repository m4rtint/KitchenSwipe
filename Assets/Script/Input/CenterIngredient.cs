using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IngredientGenerator))]
public class CenterIngredient : MonoBehaviour
{
    [SerializeField]
    GameObject UserInput;

    //Movement
    [SerializeField]
    float m_TimeToReachTarget;
    Vector3 m_StartPosition;

    //Rotation
    [SerializeField]
    float m_RotationSpeed;
    bool m_isRotateClockwise;
    
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
        RotateCenterAnimation();
    }

    #endregion

    #region GetterSetter
    Vector3 GetIngredientAtSwipedDirectionPosition()
    {
        FoodHolder holder = m_IngredientGenerator.GetFoodHolder()[(int)m_SwipedDirection];
        if (holder.GetStoredFood() != null)
        {
            Ingredient ingredient = holder.GetStoredFood().GetNeededIngredient();
            return ingredient.GetComponent<RectTransform>().position - m_StartPosition;
        } else
        {
            return holder.transform.position;
        }
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
        Vector3 m_EndPosition = GetIngredientAtSwipedDirectionPosition();
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
            MoveCenterToIngredient(m_EndPosition);
        }

    }

    void MoveCenterToIngredient(Vector3 position)
    {
        iTween.MoveBy(gameObject, iTween.Hash("x", position.x, "y", position.y, "easeType", "easeInOutExpo", "time", m_TimeToReachTarget));
        IEnumerator coroutine = WaitAndRunMoveDelegate(m_TimeToReachTarget);
        StartCoroutine(coroutine);
    }

    IEnumerator WaitAndRunMoveDelegate(float n)
    {
        yield return new WaitForSeconds(n);
        ResetCenterAnimation();
        UserInput.GetComponent<UserInput>().RunSwipeDelegate();
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
        m_StartCenterRotatationAnimation = false;
        m_isRotateClockwise = true;
    }
    
    #endregion

}
