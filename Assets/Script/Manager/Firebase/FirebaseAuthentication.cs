using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using System.Threading.Tasks;

public class FirebaseAuthentication : MonoBehaviour {
    //Delegate
    public delegate void FirebaseAuthDelegate();
    public FirebaseAuthDelegate authDelegate;
    public FirebaseAuthDelegate profileUpdateDelegate;
    public FirebaseAuthDelegate signOutDelegate;

    public delegate void FirebaseAuthErrorDelegate(string error);
    public FirebaseAuthErrorDelegate errorDelegate;

    public static FirebaseAuthentication instance = null;

    FirebaseAuth auth;
    FirebaseUser user;

    #region Mono
    private void Awake()
    {
		instance = this;
    }

	private void Start()
	{
		InitializeFirebase();
	}


    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += authStateChanged;
        authStateChanged(this, null);
    }


    void authStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
				signOutDelegate ();
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                PlayerPrefs.DeleteAll();
                authDelegate();
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= authStateChanged;
        auth = null;
    }
    #endregion

    #region Getter
    public string displayName()
    {
        return user.DisplayName;
    }

    public string email()
    {
        return user.Email;
    }

	public string userID()
	{
		return user.UserId;
	}
    #endregion

    #region Public
    //Anonymous Authentication
    public void anonAuthentication(string displayName)
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (authHasError(task))
            {
                return;
            }

            user = task.Result;

            if (user.DisplayName != displayName) { 
                updateProfile(displayName);
            }
        });
    }

    //Email Authentication
    public void emailRegistration(Dictionary<string,string> profile){
        string profileEmail = profile["email"];
        string password = profile["password"];
        auth.CreateUserWithEmailAndPasswordAsync(profileEmail, password).ContinueWith(task => {
            if (authHasError(task))
            {
                return;
            }

            // Firebase user has been created.
            user = task.Result;
			updateProfile(profile["displayname"]);
			FirebaseDB.instance.CreateNewUser(profile,user.UserId);
		
        });
    }

    public void emailAuthentication(Dictionary<string, string> profile)
    {
        string profileEmail = profile["email"];
        string password = profile["password"];
        auth.SignInWithEmailAndPasswordAsync(profileEmail, password).ContinueWith(task => {
            if (authHasError(task))
            {
                return;
            }

            user = task.Result;
			profileUpdateDelegate();
        });
    }

	public void logOut(){
		auth.SignOut ();
	}

    #endregion

    #region Helper
    void updateProfile(string profileName)
    {
        if (auth.CurrentUser != null)
        {
            UserProfile profile = new UserProfile();
            profile.DisplayName = profileName;
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (authHasError(task))
                {
                    //Error Auth
                    return;
                }

                profileUpdateDelegate();
            });
        }
    }


    bool authHasError(Task task)
    {
        bool isError = task.IsCanceled || task.IsFaulted;
        if (isError)
        {
            if (task.Exception.InnerExceptions.Count > 0)
            {

                errorDelegate(task.Exception.InnerExceptions[0].Message);
            }
            else
            {
                errorDelegate(ErrorMessages.GENERIC_ERROR);
            }
        }
        return isError;
    }

    void syncPlayerPrefs()
    {
      
    }
    #endregion

}
