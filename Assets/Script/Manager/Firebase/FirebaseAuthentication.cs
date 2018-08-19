using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class FirebaseAuthentication : MonoBehaviour {
    //Delegate
    public delegate void FirebaseAuthDelegate();
    public FirebaseAuthDelegate anonAuthDelegate;
    public FirebaseAuthDelegate emailAuthDelegate;
    public FirebaseAuthDelegate profileUpdateDelegate;

    public delegate void FirebaseAuthErrorDelegate(string error);
    public FirebaseAuthErrorDelegate errorDelegate;

    public static FirebaseAuthentication instance = null;

    FirebaseAuth m_Auth;
    FirebaseUser m_User;

    #region Mono
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitializeFirebase();
    }


    void InitializeFirebase()
    {
        m_Auth = FirebaseAuth.DefaultInstance;
        m_Auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (m_Auth.CurrentUser != m_User)
        {
            bool signedIn = m_User != m_Auth.CurrentUser && m_Auth.CurrentUser != null;
            if (!signedIn && m_User != null)
            {
                Debug.Log("Signed out " + m_User.UserId);
            }
            m_User = m_Auth.CurrentUser;
            if (signedIn)
            {
                /*
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
                photoUrl = user.PhotoUrl ?? "";
                */
                Debug.Log("Sign in");
                anonAuthDelegate();
            }
        }
    }

    void OnDestroy()
    {
        m_Auth.StateChanged -= AuthStateChanged;
        m_Auth = null;
    }
    #endregion

    #region Getter
    public string DisplayName()
    {
        return m_User.DisplayName;
    }
    #endregion

    #region Public
    public void AnonAuthentication(string displayName)
    {
        m_Auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            m_User = task.Result;
           
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                m_User.DisplayName, m_User.UserId);
            if (m_User.DisplayName != displayName) { 
                UpdateProfile(displayName);
            }
        });
    }

    public void EmailAuthentication(Dictionary<string,string> profile){
        string email = profile["email"];
        string password = profile["password"];
        m_Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    #endregion

    #region Helper
    void UpdateProfile(string displayName)
    {
        FirebaseUser user = m_Auth.CurrentUser;
        if (user != null)
        {
            UserProfile profile = new UserProfile();
            profile.DisplayName = displayName;
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                profileUpdateDelegate();
                Debug.Log("User profile updated successfully.");
            });
        }
    }
    #endregion

}
