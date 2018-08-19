using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System.Threading.Tasks;

public class FirebaseAuthentication : MonoBehaviour {
    //Delegate
    public delegate void FirebaseAuthDelegate();
    public FirebaseAuthDelegate AuthDelegate;
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
                AuthDelegate();
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

    public string Email()
    {
        return m_User.Email;
    }
    #endregion

    #region Public
    //Anonymous Authentication
    public void AnonAuthentication(string displayName)
    {
        m_Auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (AuthHasError(task))
            {
                return;
            }

            m_User = task.Result;

            if (m_User.DisplayName != displayName) { 
                UpdateProfile(displayName);
            }
        });
    }

    //Email Authentication
    public void EmailRegistration(Dictionary<string,string> profile){
        string email = profile["email"];
        string password = profile["password"];
        m_Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (AuthHasError(task))
            {
                return;
            }

            // Firebase user has been created.
            m_User = task.Result;
            UpdateProfile(profile["displayname"]);
        });
    }

    public void EmailAuthentication(Dictionary<string, string> profile)
    {
        string email = profile["email"];
        string password = profile["password"];
        m_Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (AuthHasError(task))
            {
                return;
            }

            m_User = task.Result;
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
                if (AuthHasError(task))
                {
                    return;
                }

                profileUpdateDelegate();
            });
        }
    }


    bool AuthHasError(Task task)
    {
        bool isError = task.IsCanceled || task.IsFaulted;
        if (isError)
        {
            Debug.Log("Error happened");
            if (task.Exception.InnerExceptions.Count > 0)
            {
                errorDelegate(task.Exception.InnerExceptions[0].Message);

            }
            else
            {
                errorDelegate("An Error has occured");
            }
        }
        return isError;
    }
    #endregion

}
