using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaderboardRow : MonoBehaviour {

    [SerializeField]
    Text columnLeft;
    int leftSize;

    [SerializeField]
    Text columnRight;



    public void setFontSize()
    {
        PlayerPrefs.SetInt(PlayerPrefKeys.FONTSIZE, columnLeft.cachedTextGenerator.fontSizeUsedForBestFit);
    }

    int getFontSize()
    {
       return PlayerPrefs.GetInt(PlayerPrefKeys.FONTSIZE);
    }

    public void setRowText(int number, string name, string points){
        setTMProText(columnLeft, number+". "+ name);
        setTMProText(columnRight, points);
    }

    void setTMProText(Text obj, string _name){
        obj.text = _name;
        obj.resizeTextForBestFit = false;
        obj.fontSize = getFontSize();
    }

    public void forceSetSize()
    {
        columnLeft.resizeTextForBestFit = false;
        columnRight.resizeTextForBestFit = false;
        columnLeft.fontSize = getFontSize();
        columnRight.fontSize = getFontSize();
    }

}
