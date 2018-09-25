using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMultibutton : MonoBehaviour {
    [SerializeField]
    LeaderboardManager leaderboardManager;

    [Header("Sprites")]
    [SerializeField]
    Sprite pointSprite;

    [SerializeField]
    Sprite comboSprite;

    [SerializeField]
    Sprite dishesSprite;

    [SerializeField]
    Sprite timeSprite;

    [Header("Buttons")]
    [SerializeField]
    Button points;

    [SerializeField]
    Button combo;

    [SerializeField]
    Button dishes;

    [SerializeField]
    Button time;

    private void Awake()
    {
        setupButtons();
    }

    void setupButtons()
    {
        points.onClick.AddListener(delegate {
            setSprite(pointSprite);
            leaderboardManager.sortAndSetByScore();
        });

        combo.onClick.AddListener(delegate {
            setSprite(comboSprite);
            leaderboardManager.sortAndSetByCombo();
        });

        dishes.onClick.AddListener(delegate {
            setSprite(dishesSprite);
            leaderboardManager.sortAndSetByPlates();
        });

        time.onClick.AddListener(delegate {
            setSprite(timeSprite);
            leaderboardManager.sortAndSetBySecondsLasted();
        });
    }

    void setSprite(Sprite s)
    {
        GetComponent<Image>().sprite = s;
    }

}
