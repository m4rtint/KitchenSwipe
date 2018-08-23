using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseDB : MonoBehaviour {

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
        } else if(instance != this)
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
	public void CreateNewUser(Dictionary<string,string> profile, string userID){
		User user = new User (profile ["displayname"], profile ["email"]);
		string json = JsonUtility.ToJson (user);

		reference.Child (User).Child (userID).SetRawJsonValueAsync (json);
	}

	public void SyncHighScore(string userId){
		FirebaseDatabase.DefaultInstance
			.GetReference("Leaderboard").Child(userId)
			.GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {
					// Handle the error...
				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					int score = int.Parse(snapshot.Child("Score").Value.ToString());
					int plates = int.Parse(snapshot.Child("Plates").Value.ToString());
					PlayerPrefs.SetInt("InfiniteMode",score);
					PlayerPrefs.SetInt("Infinte_Plate",plates);
				}
			});
	}

	public void InsertScoreEntry(int score, int plates){
		string uid = FirebaseAuthentication.instance.UserID ();
		string displayName = FirebaseAuthentication.instance.DisplayName ();

		Leaderboard entry = new Leaderboard (displayName, score, plates);
		string json = JsonUtility.ToJson (entry);
		reference.Child (Leaderboard).Child (uid).SetRawJsonValueAsync (json);
	}
    #endregion
}
