using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IngredientGenerator))]
public class CenterIngredient : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    GameObject UserInput;
    [SerializeField]
    GameObject m_IngredientGeneratorObj;
    IngredientGenerator m_IngredientGenerator;

    [Header("Movement")]
    [SerializeField]
    float m_TimeToReachTarget;
    Vector3 m_StartPosition;
    [SerializeField]
    float m_SauceFadeTime;
    


    Ingredient m_CenterIngredient;
    Direction m_SwipedDirection;


    #region Mono
    // Use this for initialization
    void Awake()
    {
        m_StartPosition = transform.position;
        m_IngredientGenerator = m_IngredientGeneratorObj.GetComponent<IngredientGenerator>();
        SetupDelegate();
    }

    void SetupDelegate()
    {
        UserInput.GetComponent<UserInput>().snapOffDelegate += StartCenterAnimation;
    }

    #endregion

    #region GetterSetter
    Vector3 GetIngredientAtSwipedDirectionPosition()
    {
        FoodHolder holder = m_IngredientGenerator.GetFoodHolder()[(int)m_SwipedDirection];
        Food food = holder.GetStoredFood();
        if (food != null)
        {
            if (food.GetIngredientLevel() >= 0){
                Ingredient ingredient = holder.GetStoredFood().GetNeededIngredient();
                return ingredient.GetComponent<RectTransform>().position - m_StartPosition;
            }
        } 

        return holder.GetComponent<RectTransform>().position - m_StartPosition;

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
            RotateCenterIngredient(dir);
        }
        else
        {
            MoveCenterToIngredient(m_EndPosition);
        }

    }

    void RotateCenterIngredient(Direction dir)
    {
        Hashtable ht = new Hashtable();
        ht.Add("z", AngleForRotation(dir));
        ht.Add("easeType", "easeInOutBack");
        ht.Add("time", m_TimeToReachTarget);
        ht.Add("oncomplete", "FadeOutCenter");
        iTween.RotateBy(gameObject, ht);
    }

    float AngleForRotation(Direction dir)
    {
        float angle = 40;
        switch (dir)
        {
            case Direction.Down:
                angle = -140; break;
            case Direction.Up:
                angle = 40; break;
            case Direction.Left:
                angle = 130; break;
            case Direction.Right:
                angle = -50;break;
        }
        return angle / 360;
    }

    void FadeOutCenter()
    {
        Hashtable ht = new Hashtable();
        ht.Add("a", 0);
        ht.Add("time", m_SauceFadeTime);
        ht.Add("oncomplete", "WaitAndRunMoveDelegate");
        iTween.ColorTo(gameObject, ht);
    }

    void MoveCenterToIngredient(Vector3 position)
    {
        Hashtable ht = new Hashtable();
        ht.Add("x", position.x);
        ht.Add("y", position.y);
        ht.Add("easeType", "easeInOutExpo");
        ht.Add("time", m_TimeToReachTarget);
        ht.Add("oncomplete", "WaitAndRunMoveDelegate");
        iTween.MoveBy(gameObject, ht);
    }

    void WaitAndRunMoveDelegate()
    {
        ResetCenterAnimation();
        UserInput.GetComponent<UserInput>().RunSwipeDelegate();
    }

    void ResetCenterAnimation()
    {
        GetComponent<Image>().color = Color.white;
        transform.position = m_StartPosition;
        transform.rotation = Quaternion.identity;
    }
    
    #endregion

}
