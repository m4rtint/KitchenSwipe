using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;

public class FirebaseDatabase : MonoBehaviour {

    public static FirebaseDatabase instance = null;
    readonly string DB_URL = "https://kitchen-swipe.firebaseio.com/";

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
    }
    #endregion
}
