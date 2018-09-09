using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileManager : MonoBehaviour {

    [SerializeField]
    GameObject points;

    [SerializeField]
    GameObject combo;

    [SerializeField]
    GameObject dishes;

    [SerializeField]
    GameObject secondsLasted;

    private void Start()
    {
        FirebaseDB.instance.profileDelegate += onLoadProfile;
    }


    void onLoadProfile()
    {
        setUIText(points, PlayerPrefKeys.INFINITE_SCORE);
        setUIText(combo, PlayerPrefKeys.INFINITE_COMBO);
        setUIText(dishes, PlayerPrefKeys.INFINITE_DISHES);
        setUIText(secondsLasted, PlayerPrefKeys.INFINITE_SECONDS);
    }

    void setUIText(GameObject property, string key)
    {
        property.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(key).ToString();
    }
}
