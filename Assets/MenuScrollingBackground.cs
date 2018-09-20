using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MenuScrollingBackground : MonoBehaviour {

    [SerializeField]
    bool goLeft;

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
        groundHorizontalLength = GetComponent<RectTransform>().rect.width;
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (goLeft)
        {
            if (transform.position.x < -groundHorizontalLength * 0.5f)
            {
                respositionBackground();
            }
        } else {
            if (transform.position.x > groundHorizontalLength * 1.5f)
            {
                respositionBackground();
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
