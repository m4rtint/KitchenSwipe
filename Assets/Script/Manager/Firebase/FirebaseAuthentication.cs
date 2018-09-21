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
				signOutDelegate ();
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                PlayerPrefs.DeleteAll();
                FirebaseDB.instance.syncPlayerPrefs(userID());
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
    {   Debug.Log(PlayerPrefs.GetString(PlayerPrefKeys.DISPLAYNAME));
        return PlayerPrefs.GetString(PlayerPrefKeys.DISPLAYNAME);
    }

    void displayName(string name)
    {
        PlayerPrefs.SetString(PlayerPrefKeys.DISPLAYNAME, name);
    }

	public string userID()
	{
		return user.UserId;
	}
    #endregion

    #region Public
    //Anonymous Authentication
    public void anonAuthentication()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (!authHasError(task))
            {
                user = task.Result;
                displayName(generateRandomName());
                updateProfile();
            }
        });
    }

    //Email Authentication
    public void emailRegistration(Dictionary<string,string> profile){
        string profileEmail = profile["email"];
        string password = profile["password"];
        auth.CreateUserWithEmailAndPasswordAsync(profileEmail, password).ContinueWith(task => {
            if (!authHasError(task))
            {
                // Firebase user has been created.
                user = task.Result;
                displayName(profile["displayname"]);
                updateProfile();
                FirebaseDB.instance.CreateNewUser(profile, user.UserId);
            }
        });
    }

    public void emailAuthentication(Dictionary<string, string> profile)
    {
        string profileEmail = profile["email"];
        string password = profile["password"];
        auth.SignInWithEmailAndPasswordAsync(profileEmail, password).ContinueWith(task => {
            if (!authHasError(task))
            {
                user = task.Result;
                updateProfile();
            }
        });
    }

	public void logOut(){
		auth.SignOut ();
	}

    #endregion

    #region Helper
    void updateProfile()
    {
        if (user.DisplayName == "" )
        {
            UserProfile profile = new UserProfile();
            profile.DisplayName = displayName();
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (!authHasError(task))
                {
                    profileUpdateDelegate();
                }
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

    string generateRandomName()
    {
        string result = "Guest ";
        string num1 = Random.Range(0, 500).ToString();
        string num2 = Random.Range(500, 999).ToString();
        return result + num1.ToString() + num2.ToString();
    }
    #endregion

}
