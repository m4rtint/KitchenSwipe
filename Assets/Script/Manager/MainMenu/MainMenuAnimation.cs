using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour {

    [SerializeField]
    GameObject[] scrolls;

    [SerializeField]
    float speed;

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
            obj.GetComponent<MenuScrollingBackground>().setScrollingSpeed(speed);
        }
    }
    #endregion



}
