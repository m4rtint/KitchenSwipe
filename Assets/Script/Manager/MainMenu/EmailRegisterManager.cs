using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailRegisterManager : MonoBehaviour {

    [SerializeField]
    InputField displayName;

    [SerializeField]
    InputField email;

    [SerializeField]
    InputField password;

    public void emailRegistration()
    {
        Dictionary<string, string> profile = new Dictionary<string, string>();
        profile["email"] = email.text;
        profile["password"] = password.text;
        profile["displayname"] = displayName.text;
        FirebaseAuthentication.instance.emailRegistration(profile);
    }
}
