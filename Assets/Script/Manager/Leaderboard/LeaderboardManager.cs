using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour {
    [SerializeField]
    GameObject userRow;

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
        getRowHeight();
        clearLeaderboard();
    }

    void getRowHeight()
    {
        rowHeight = row.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void OnEnable()
    {
        FirebaseDB.instance.loadedLeaderboardDelegate += loadedLeaderboardStats;

        clearLeaderboard();
        setLeaderboardScreen(true);
        FirebaseDB.instance.LoadHighScore();
    }

    private void OnDisable()
    {
        FirebaseDB.instance.loadedLeaderboardDelegate -= loadedLeaderboardStats;
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

        int placing = 1;
        string score = "0";
        for (int i = 0; i < rows.Length; i++)
        {
            placing = i + 1;
            Record record = records[i];
            score = record.Score.ToString();
            rows[i].GetComponent<LeaderboardRow>().setRowText(i + 1, records[i].Name, records[i].Score.ToString());
            setUserScoreIfNeeded(records[i], placing, score);
        }
    }

    public void sortAndSetByPlates()
    {
        Array.Sort(records,
           delegate (Record x, Record y)
           {
               return y.Dishes.CompareTo(x.Dishes);
           });

        int placing = 1;
        string score = "0";
        for (int i = 0; i < rows.Length; i++)
        {
            placing = i + 1;
            Record record = records[i];
            score = record.Dishes.ToString();
            rows[i].GetComponent<LeaderboardRow>().setRowText(i + 1, records[i].Name, records[i].Dishes.ToString());
            setUserScoreIfNeeded(records[i], placing, score);
        }
    }

    public void sortAndSetByCombo()
    {
        Array.Sort(records,
           delegate (Record x, Record y)
           {
               return y.Combo.CompareTo(x.Combo);
           });

        int placing = 1;
        string score = "0";
        for (int i = 0; i < rows.Length; i++)
        {
            placing = i + 1;
            Record record = records[i];
            score = record.Combo.ToString();
            rows[i].GetComponent<LeaderboardRow>().setRowText(i + 1, records[i].Name, records[i].Combo.ToString());
            setUserScoreIfNeeded(records[i], placing, score);
        }
    }

    public void sortAndSetBySecondsLasted()
    {
        Array.Sort(records,
           delegate (Record x, Record y)
           {
               return y.TimeLasted.CompareTo(x.TimeLasted);
           });

        int placing = 1;
        string score = "0";
        for (int i = 0; i < rows.Length; i++)
        {
            placing = i + 1;
            Record record = records[i];
            score = record.TimeLasted.ToString();
            rows[i].GetComponent<LeaderboardRow>().setRowText(placing, record.Name, score);
            setUserScoreIfNeeded(records[i], placing, score);
        }
    }

    void setUserScoreIfNeeded(Record record, int index, string score)
    {
        if (record.key == FirebaseAuthentication.instance.userID())
        {
            setUserHighScores(index, score);
        }
    }
    #endregion

    #region helper
    void setUserHighScores(int number, string type)
    {
        userRow.GetComponent<LeaderboardRow>().setRowText(number, FirebaseAuthentication.instance.displayName(), type);
    }
    #endregion

}
