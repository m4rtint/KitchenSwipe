using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LeaderboardRow : MonoBehaviour {

    [SerializeField]
    GameObject columnLeft;

    [SerializeField]
    GameObject columnRight;

    public void setRowText(string name, string points){
        setTMProText(columnLeft, name);
        setTMProText(columnRight, points);
    }

    void setTMProText(GameObject obj, string _name){
        obj.GetComponent<TextMeshProUGUI>().text = _name; 
    }

}
