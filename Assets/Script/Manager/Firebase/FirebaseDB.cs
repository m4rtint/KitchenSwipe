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

    public static FirebaseDB instance = null;
    readonly string DB_URL = "https://kitchen-swipe.firebaseio.com/";
    readonly string User = "Users";
    readonly string Leaderboard = "Leaderboard";

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

        reference.Child(User).Child(userID).SetRawJsonValueAsync(json);
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

    public void insertScoreEntry(int score, int plates, int combo, int timeLasted)
    {
        string uid = FirebaseAuthentication.instance.userID();
        string displayName = FirebaseAuthentication.instance.displayName();

        Record entry = new Record(displayName, score, plates, combo, timeLasted);
        string json = JsonUtility.ToJson(entry);
        reference.Child(Leaderboard).Child(uid).SetRawJsonValueAsync(json);
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
            string _name = parseSnapshotString(obj, "Name");
            int _score = parseSnapshotInteger(obj, "Score");
            int _plates = parseSnapshotInteger(obj, "Plates");
            int _combo = parseSnapshotInteger(obj, "Combo");
            int _TimeLasted = parseSnapshotInteger(obj, "TimeLasted");
            listOfRecords.Add(new Record(_name, _score, _plates, _combo, _TimeLasted));
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

    #endregion
}