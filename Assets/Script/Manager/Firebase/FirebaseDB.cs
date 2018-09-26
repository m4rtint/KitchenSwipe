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
        User user = new User(profile[PlayerPrefKeys.DISPLAYNAME], profile["email"]);
        string json = JsonUtility.ToJson(user);

        reference.Child(USER).Child(userID).SetRawJsonValueAsync(json);
    }

    public void LoadHighScore()
    {
        FirebaseDatabase.DefaultInstance
                        .GetReference(LEADERBOARD)
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
            .GetReference(LEADERBOARD).Child(userId)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    if (snapshot.HasChildren)
                    {
                        Debug.Log("Found Database children");
                        parseSnapToPlayerPrefs(snapshot);
                        profileDelegate();
                    }
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
            string _name = parseSnapshotString(obj,NAME);

            Dictionary<string, int> profile = parseProfile(obj);

            listOfRecords.Add(new Record(_key, _name, profile[SCORE], profile[DISHES], profile[COMBO], profile[TIMELASTED]));
        }

        Record[] records = new Record[listOfRecords.Count];
        listOfRecords.CopyTo(records);
        return records;
    }

    void parseSnapToPlayerPrefs(DataSnapshot snap)
    {
        Dictionary<string, int> profile = parseProfile(snap);

        string name = parseSnapshotString(snap, NAME);

        PlayerPrefs.SetString(PlayerPrefKeys.DISPLAYNAME, name);
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SCORE, profile[SCORE]);
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_DISHES, profile[DISHES]);
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_COMBO, profile[COMBO]);
        PlayerPrefs.SetInt(PlayerPrefKeys.INFINITE_SECONDS, profile[TIMELASTED]);
    }

    string parseSnapshotString(DataSnapshot snap, string key)
    {
        if (snap.Child(key).Exists) {
            return (string)snap.Child(key).Value;
        } else
        {
            return "N/A";
        }
    }

    int parseSnapshotInteger(DataSnapshot snap, string key)
    {
        if (snap.Child(key).Exists) {
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

    Dictionary<string, int> parseProfile(DataSnapshot snap)
    {
        Dictionary<string, int> ht = new Dictionary<string, int>();

        ht.Add(SCORE, parseSnapshotInteger(snap, SCORE));
        ht.Add(DISHES, parseSnapshotInteger(snap, DISHES));
        ht.Add(COMBO, parseSnapshotInteger(snap, COMBO));
        ht.Add(TIMELASTED, parseSnapshotInteger(snap, TIMELASTED));

        return ht;
    }

    #endregion
}