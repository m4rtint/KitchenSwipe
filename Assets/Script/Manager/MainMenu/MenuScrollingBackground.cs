using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class MenuScrollingBackground : MonoBehaviour {

    [SerializeField]
    bool goLeft;

    [SerializeField]
    bool isSecond;

    //Reposition properties
    float groundHorizontalLength;

    //Scrolling properties
    Rigidbody2D rigidBody;

    float positionLimit;

    public float Direction()
    {
        return goLeft ? 1 : -1; 
    }

    #region Mono
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (groundHorizontalLength == 0)
        {
            groundHorizontalLength = GetComponent<RectTransform>().rect.width;
        }

        if (goLeft)
        {
            if (!isSecond)
            {
                if (transform.position.x < 0)
                {
                    respositionBackground();
                }

            } else
            {
                if (transform.position.x < -groundHorizontalLength)
                {
                    respositionBackground();
                }
            }
            
        } else {
            RectTransform trans = GetComponent<RectTransform>();
            if (!isSecond)
            {
                if (trans.position.x > Screen.width)
                {
                    respositionBackground();
                }
            }
            else
            {
                if (trans.position.x > Screen.width + groundHorizontalLength)
                {
                    respositionBackground();
                }
            }
        }
    }
    #endregion

    #region Scrolling
    public void setScrollingSpeed(float speed)
    {
        rigidBody.velocity = new Vector2(-speed * Direction(), 0);
    }


    private void respositionBackground()
    {
        Vector2 groundOffSet = new Vector2(groundHorizontalLength * 2f * Direction(), 0);
        transform.position = (Vector2)transform.position + groundOffSet;
    }
    #endregion

}
