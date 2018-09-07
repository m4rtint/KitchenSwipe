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

    public void SyncHighScore(string userId)
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("Leaderboard").Child(userId)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    int score = int.Parse(snapshot.Child("Score").Value.ToString());
                    int plates = int.Parse(snapshot.Child("Plates").Value.ToString());
                    PlayerPrefs.SetInt("InfiniteMode", score);
                    PlayerPrefs.SetInt("Infinte_Plate", plates);
                }
            });
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

    string parseSnapshotString(DataSnapshot snap, string key)
    {
        if (snap.Child(key).Exists) {
            Debug.Log("FirebaseDB: Successfully parsed: " + key);
            return (string)snap.Child(key).Value;
        } else
        {
            return "NA";
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