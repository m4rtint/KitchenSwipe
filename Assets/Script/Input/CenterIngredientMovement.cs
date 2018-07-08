using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterIngredientMovement : MonoBehaviour
{
    [SerializeField]
    float m_TimeToReachTarget;
    float m_Speed;
    Vector3 m_StartPosition;
    Vector3 m_EndPosition;

    bool m_StartSnapOffScreenAnimation;
    

    #region Mono
    // Use this for initialization
    void Awake()
    {
        Debug.Log("Start Position:" + transform.position);
        m_StartPosition = transform.position;
        m_StartSnapOffScreenAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        SnapOffScreenAnimation();
    }

    #endregion

    #region GetterSetter
    public void StartSnapOffScreenAnimation(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                m_EndPosition = new Vector3(m_StartPosition.x,Screen.height*1.2f);
                break;
            case Direction.Down:
                m_EndPosition = new Vector3(m_StartPosition.x, -Screen.height*0.1f);
                break;
            case Direction.Left:
                m_EndPosition = new Vector3(-Screen.width * 0.2f, m_StartPosition.y);
                break;
            case Direction.Right:
                m_EndPosition = new Vector3(Screen.width*1.2f, m_StartPosition.y);
                break;
            default:
                break;
        }
        m_StartSnapOffScreenAnimation = true;
    }

    public float TimeToReachTarget()
    {
        return m_TimeToReachTarget;
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
                GetComponent<UserInput>().RunSwipeDelegate();
            }
        }
    }
    #endregion

}
