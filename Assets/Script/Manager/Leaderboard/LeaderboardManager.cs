using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour {
    [SerializeField]
    GameObject title;

    [Header("Leaderboard")]
    [SerializeField]
    GameObject scores;
    [SerializeField]
    GameObject row;

    [Header("Loading")]
    [SerializeField]
    GameObject loading;

    GameObject[] leaderboardRows;
    float rowHeight;

    Record[] records;
    GameObject[] rows;

    #region mono
    private void Awake()
    {
        setLeaderboardScreen(true);
        FirebaseDB.instance.loadedLeaderboardDelegate += loadedLeaderboardStats;
        getRowHeight();
        clearLeaderboard();
    }

    void getRowHeight()
    {
        rowHeight = row.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void OnEnable()
    {
        clearLeaderboard();
        setLeaderboardScreen(true);
        FirebaseDB.instance.LoadHighScore();
        animateLoading();
    }

    void setLeaderboardScreen(bool isLoading)
    {
        loading.SetActive(isLoading);
        scores.SetActive(!isLoading);
    }
    #endregion

    #region SetLeaderboard
    void loadedLeaderboardStats(Record[] list)
    {
        saveRecords(list);
        insertGameObjectRow();
        sortAndSetByScore();
        setLeaderboardScreen(false);
    }

    void saveRecords(Record[] list)
    {
        records = list;
        rows = new GameObject[list.Length];
    }

    void insertGameObjectRow()
    {
        for (int i = 0; i < records.Length; i++)
        {
            GameObject newRow = Instantiate(row, scores.transform);

            //Set height
            float height = -i * (rowHeight * 2);
            Vector3 newPosition = newRow.transform.position;
            newPosition.y += height;
            newRow.transform.position = newPosition;

            //Save new row
            rows[i] = newRow; 
        }
    }

    void clearLeaderboard()
    {
        foreach(Transform child in scores.transform)
        {
            Destroy(child.gameObject);
        }
    }
    #endregion

    #region Public
    public void sortAndSetByScore()
    {
        Array.Sort(records,
           delegate (Record x, Record y)
           {
               return y.Score.CompareTo(x.Score);
           });
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].GetComponent<LeaderboardRow>().setRowText(i + 1, records[i].Name, records[i].Score.ToString());
        }
        setLeaderboardTitle("POINTS");
    }

    public void sortAndSetByPlates()
    {
        Array.Sort(records,
           delegate (Record x, Record y)
           {
               return y.Dishes.CompareTo(x.Dishes);
           });
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].GetComponent<LeaderboardRow>().setRowText(i + 1, records[i].Name, records[i].Dishes.ToString());
        }
        setLeaderboardTitle("PLATES");
    }

    public void sortAndSetByCombo()
    {
        Array.Sort(records,
           delegate (Record x, Record y)
           {
               return y.Combo.CompareTo(x.Combo);
           });
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].GetComponent<LeaderboardRow>().setRowText(i + 1, records[i].Name, records[i].Combo.ToString());
        }
        setLeaderboardTitle("COMBO");
    }

    public void sortAndSetBySecondsLasted()
    {
        Array.Sort(records,
           delegate (Record x, Record y)
           {
               return y.TimeLasted.CompareTo(x.TimeLasted);
           });
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].GetComponent<LeaderboardRow>().setRowText(i + 1, records[i].Name, records[i].TimeLasted.ToString());
        }
        setLeaderboardTitle("SECOND LASTED");
    }
    #endregion

    #region helper
    void animateLoading()
    {
        Hashtable ht = new Hashtable();
        ht.Add("z", 1);
        ht.Add("easeType", "easeInOutBack");
        ht.Add("looptype", "loop");
        iTween.RotateBy(loading, ht);
    }

    void setLeaderboardTitle(string type)
    {
        title.GetComponent<TextMeshProUGUI>().text = type;
    }
    #endregion

}
