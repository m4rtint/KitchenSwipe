using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class FbAnalytics : MonoBehaviour
{
    public static FbAnalytics instance = null;

    #region Mono
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Base
    private void logEvent(string eventName, Dictionary<string, string> dict)
    {
        Firebase.Analytics.Parameter[] parameters = createFirebaseParam(dict);
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName, parameters);
    }

    private void logEvent(string eventName) {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName);
    }

    private Firebase.Analytics.Parameter[] createFirebaseParam(Dictionary<string, string> dict)
    {
        Firebase.Analytics.Parameter[] parameter = new Firebase.Analytics.Parameter[dict.Count];
        int index = 0;
        foreach (KeyValuePair<string, string> entry in dict)
        {
            parameter.SetValue(new Firebase.Analytics.Parameter(entry.Key, entry.Value), index);
            index++;
        }

        return parameter;
    }
    #endregion

    #region public
    public void wrongSwipe(Direction direction, string center, string side)
    {
        Dictionary<string, string> param = new Dictionary<string, string> {
            { "side_food", side},
            { "center_food", center},
            { "direction", getDirection(direction)}
        };

        logEvent("wrong_swipe", param);
    }

    public void gameResult(int score, int dishes, int totalSeconds, int combo) {
        Dictionary<string, string> param = new Dictionary<string, string> {
            { "score", score.ToString() },
            { "dishes", dishes.ToString() },
            { "total_seconds", totalSeconds.ToString() },
            { "combo", combo.ToString() }
        };

        logEvent("game_results", param);
    }

    public void openedLeaderboard() {
        logEvent("open_leaderboard");
    }

    public void timeBeforeQuit(int seconds) {
        Dictionary<string, string> param = new Dictionary<string, string> {
            { "quitting_time", seconds.ToString() }
        };
        logEvent("time_before_quit", param);
    }
    #endregion

    #region Tools
    private string getDirection(Direction dir)
    {
        string direction = "up";
        switch (dir)
        {
            case Direction.Up:
                direction = "up";
                break;
            case Direction.Down:
                direction = "down";
                break;
            case Direction.Left:
                direction = "left";
                break;
            case Direction.Right:
                direction = "right";
                break;
        }

        return direction;
    }
    #endregion
}
