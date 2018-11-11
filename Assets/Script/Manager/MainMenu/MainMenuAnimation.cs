using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour {

    [SerializeField]
    GameObject[] scrolls;

    readonly float speedRatio = 2.88f;

    #region Mono
    private void Start()
    {
        setSpeedOfScrolling();
    }
    #endregion

    #region Speed
    void setSpeedOfScrolling()
    {
        foreach (GameObject obj in scrolls)
        {
            float speed = Screen.width / speedRatio;
            obj.GetComponent<MenuScrollingBackground>().setScrollingSpeed(speed);
        }
    }
    #endregion



}
