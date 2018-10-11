using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAnimation : MonoBehaviour {

    [SerializeField]
    GameObject leftArrow;

    [SerializeField]
    GameObject rightArrow;

    [SerializeField]
    GameObject finger;
    Vector3 originalPosition;

    readonly string swipeName = "SwipeAnim";

    readonly float moveAmount = 200;

    private void Awake()
    {
        originalPosition = finger.gameObject.transform.position;
        setArrowActive(true);
    }

    public void swipe(bool left) {
        stopAnimation();
        resetPosition(left);

        float amount = moveAmount;

        if (left) {
            amount *= -1f;
        }

        Vector3 direction = new Vector3(amount, 0, 0);
        Hashtable ht = new Hashtable();
        ht.Add("name", swipeName);
        ht.Add("amount", direction);
        ht.Add("time", 1.0f);
        ht.Add("looptype", "loop");
        iTween.MoveAdd(finger, ht);
    }

    void setArrowActive(bool left) {
        leftArrow.SetActive(left);
        rightArrow.SetActive(!left);
    }

    void resetPosition(bool left) {
        setArrowActive(left);
        int rotate = left ? 180 : 0;

        finger.transform.position = originalPosition;
    }

    void stopAnimation() {
        iTween.StopByName(swipeName);
    }
}
