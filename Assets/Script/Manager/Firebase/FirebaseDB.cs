using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseDB : MonoBehaviour
{
    //Delegate
    public delegate void FirebaseDBDelegate(Record[] list);
    public FirebaseDBDelegate loadedLeaderboardDelegate;

    public delegate void FirebaseDBErrorDelegate(string errorMessage);
    public FirebaseDBErrorDelegate errorDelegate;

    public delegate void FirebaseDBProfileDelegate();
    public FirebaseDBProfileDelegate profileDelegate;

    public static FirebaseDB instance = null;
    readonly string DB_URL = "https://kitchen-swipe.firebaseio.com/";
    readonly string USER = "Users";
    readonly string LEADERBOARD = "Leaderboard";

    //Properties
    readonly string NAME = "Name";
    readonly string SCORE = "Score";
    readonly string DISHES = "Dishes";
    readonly string COMBO = "Combo";
    readonly string TIMELASTED = "TimeLasted";

    DatabaseReference reference;

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
        InitializeFirebase();
    }

    void InitializeFirebase()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DB_URL);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    #endregion

    #region Public Interface
    public void CreateNewUser(Dictionary<string, string> profile, string userID)
    {
        User user = new User(profile["displayname"], profile["email"]);
        string json = JsonUtility.ToJson(user);

        reference.Child(USER).Child(userID).SetRawJsonValueAsync(json);
    }

    public void LoadHighScore()
    {
        FirebaseDatabase.DefaultInstance
                        .GetReference("Leaderboard")
                        .GetValueAsync().ContinueWith(task =>
                        {
                            if (task.IsCompleted)
                            {
                                DataSnapshot snapshot = task.Result;
                                loadedLeaderboardDelegate(createArrayOfRecords(snapshot));
                            }
                            else
                            {
                                //Handle errors
                                errorDelegate(ErrorMessages.LOAD_LEADERBOARD);
                            }
                        });
    }


    public void insertScoreEntry(int score)
    {
        LeaderboardRef().Child(SCORE).SetValueAsync(score);
    }

    public void insertDishesEntry(int dishes)
    {
        LeaderboardRef().Child(DISHES).SetValueAsync(dishes);
    }

    public void insertComboEntry(int combo)
    {
        LeaderboardRef().Child(COMBO).SetValueAsync(combo);
    }

    public void insertTimeEntry(int timeLasted)
    {
        LeaderboardRef().Child(TIMELASTED).SetValueAsync(timeLasted);
    }

    public void syncPlayerPrefs(string userId)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("Leaderboard").Child(userId)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    parseSnapToPlayerPrefs(snapshot);
                    profileDelegate();
                } else
                {
                    errorDelegate(ErrorMessages.SYNC_ERROR);
                }
            });
    }

    #endregion

    #region Helper
    Record[] createArrayOfRecords(DataSnapshot snap)
    {
        ArrayList listOfRecords = new ArrayList();

        foreach (DataSnapshot obj in snap.Children)
        {
            string _key = obj.Key;
            string _name = parseSnapshotString(obj, "Name");
            int _score = parseSnapshotInteger(obj, "Score");
            int _dishes = parseSnapshotInteger(obj, "Dishes");
            int _combo = parseSnapshotInteger(obj, "Combo");
            int _TimeLasted = parseSnapshotInteger(obj, "TimeLasted");
            listOfRecords.Add(new Record(_key, _name, _score, _dishes, _combo, _TimeLasted));
        }

        Record[] records = new Record[listOfRecords.Count];
        listOfRecords.CopyTo(records);
        return records;
    }

    void parseSnapToPlayerPrefs(DataSnapshot snap)
    {
        int score = parseSnapshotInteger(snap, "Score");
        int dishes = parseSnapshotInteger(snap, "Dishes");
        int combo = parseSnapshotInteger(snap, "Combo");
        int secondsLasted = parseSnapshotInteger(snap, "TimeLasted");

        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SCORE, score);
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_DISHES, dishes);
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_COMBO, combo);
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SECONDS, secondsLasted);
    }

    string parseSnapshotString(DataSnapshot snap, string key)
    {
        if (snap.Child(key).Exists) {
            Debug.Log("FirebaseDB: Successfully parsed: " + key);
            return (string)snap.Child(key).Value;
        } else
        {
            return "N/A";
        }
    }

    int parseSnapshotInteger(DataSnapshot snap, string key)
    {
        if (snap.Child(key).Exists) {
            Debug.Log("FirebaseDB: Successfully parsed: " + key);
            return int.Parse(snap.Child(key).Value.ToString());
        } else
        {
            return 0;
        }
    }

    DatabaseReference LeaderboardRef()
    {
        string uid = FirebaseAuthentication.instance.userID();
        string displayName = FirebaseAuthentication.instance.displayName();

        reference.Child(LEADERBOARD).Child(uid).Child(NAME).SetValueAsync(displayName);
        return reference.Child(LEADERBOARD).Child(uid);
    }

    #endregion
}