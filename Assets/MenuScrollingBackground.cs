using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScrollingBackground : MonoBehaviour {

    //Reposition properties
    float groundHorizontalLength;

    //Scrolling properties
    Rigidbody2D rigidBody;

    #region Mono
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundHorizontalLength = GetComponent<RectTransform>().rect.width;
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        setScrollingSpeed(100);
    }

    private void Update()
    {
        if (transform.position.x < -groundHorizontalLength * 1.5)
        {
            respositionBackground();
        }
    }

    #endregion

    #region Scrolling
    public void setScrollingSpeed(float speed)
    {
        rigidBody.velocity = new Vector2(-speed, 0);
    }


    private void respositionBackground()
    {
        Vector2 groundOffSet = new Vector2(groundHorizontalLength * 3f, 0);
        transform.position = (Vector2)transform.position + groundOffSet;
    }

    #endregion

}
