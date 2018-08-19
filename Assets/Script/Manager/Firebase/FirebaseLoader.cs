using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseLoader : MonoBehaviour {

    [SerializeField]
    GameObject Authentication;

    [SerializeField]
    GameObject Database;

    private void Awake()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InstantiateFirebaseModules();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    void InstantiateFirebaseModules()
    {
        if (FirebaseAuthentication.instance == null)
        {
            Instantiate(Authentication);
        }

        if (FirebaseDatabase.instance == null)
        {
            Instantiate(Database);
        }
    }

}
