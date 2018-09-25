using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LeaderboardRow : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI columnLeft;

    [SerializeField]
    TextMeshProUGUI columnRight;

    public void setRowText(int number, string name, string points){
        setTMProText(columnLeft, number+" "+ name);
        setTMProText(columnRight, points);
    }

    void setTMProText(TextMeshProUGUI obj, string _name){
        obj.text = _name;
    }

}
