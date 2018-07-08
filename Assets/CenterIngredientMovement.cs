using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterIngredientMovement : MonoBehaviour
{
    [SerializeField]
    float m_SnapBackSpeed;

    Vector3 m_StartPosition;
    bool m_StartAnimation;

    #region Mono
    // Use this for initialization
    void Awake()
    {
        m_StartPosition = transform.position;
        m_StartAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_StartAnimation)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_StartPosition, Time.deltaTime * m_SnapBackSpeed);
            if (transform.position == m_StartPosition)
            {
                m_StartAnimation = false;
            }
        }
    }
    #endregion

    #region Setter
    public void StartAnimation()
    {
        m_StartAnimation = true;
    }
    #endregion

    #region Movement
    void Movement(Direction dir){
        Vector3 position = transform.position;

        if (dir == Direction.Up || dir == Direction.Down){
            transform.position = new Vector3(position.x, Input.mousePosition.y);
        } else {
            
        }

        switch(dir){
            case Direction.Up:
            case Direction.Down:
                transform.position = new Vector3(position.x, Input.mousePosition.y);
                break;
            case Direction.Left:
                break;
            case Direction.Right:
                break;
            default:
                break;
        }
    }
    #endregion

}
