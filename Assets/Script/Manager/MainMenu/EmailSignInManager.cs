using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailSignInManager : MonoBehaviour {

    [SerializeField]
    InputField email;

    [SerializeField]
    InputField password;

    public void emailSignIn()
    {
        Dictionary<string, string> profile = new Dictionary<string, string>();
        profile["email"] = email.text;
        profile["password"] = password.text;
        FirebaseAuthentication.instance.emailAuthentication(profile);
    }

}
