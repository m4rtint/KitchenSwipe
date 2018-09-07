using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour {

    [Header("Leaderboard")]
    [SerializeField]
    GameObject scores;
    [SerializeField]
    GameObject row;

    [Header("Loading")]
    [SerializeField]
    GameObject loading;
    [SerializeField]
    float spinSpeed;

    GameObject[] leaderboardRows;
    float rowHeight;

    #region mono
    private void Awake()
    {
        setLeaderboardScreen(true);
        FirebaseDB.instance.loadedLeaderboardDelegate += setLeaderboardText;
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
    void setLeaderboardText(Record[] list)
    {
        for(int i = 0; i < list.Length; i++)
        {
            insertRow(list[i], i);
        }

        setLeaderboardScreen(false);
    }

    void insertRow(Record record, int index)
    {
        GameObject newRow = Instantiate(row, scores.transform);

        //Set height
        float height = -index * (rowHeight*2);
        Vector3 newPosition = newRow.transform.position;
        newPosition.y += height;
        newRow.transform.position = newPosition;
        newRow.GetComponent<LeaderboardRow>().setRowText(index+1, record.Name, record.Score.ToString());
    }

    void clearLeaderboard()
    {
        foreach(Transform child in scores.transform)
        {
            Destroy(child.gameObject);
        }
    }
    #endregion

    #region Loading
    void animateLoading()
    {
        Hashtable ht = new Hashtable();
        ht.Add("z", 1);
        ht.Add("easeType", "easeInOutBack");
        ht.Add("looptype", "loop");
        iTween.RotateBy(loading, ht);
    }
    #endregion
}
