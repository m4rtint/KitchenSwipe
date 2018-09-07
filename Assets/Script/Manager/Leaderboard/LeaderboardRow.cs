using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LeaderboardRow : MonoBehaviour {

    [SerializeField]
    GameObject columnLeft;

    [SerializeField]
    GameObject columnRight;

    public void setRowText(int number, string name, string points){
        setTMProText(columnLeft, number+" "+ name);
        setTMProText(columnRight, points);
    }

    void setTMProText(GameObject obj, string _name){
        obj.GetComponent<TextMeshProUGUI>().text = _name; 
    }

}
