using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject DisplayNameObject;

    #region Mono
    private void Awake()
    {
        SetupDelegate();
    }

    void SetupDelegate()
    {
        FirebaseAuthentication.instance.profileUpdateDelegate += UpdateDisplayName;
    }
    #endregion

    #region Delegate
    void UpdateDisplayName()
    {
        DisplayNameObject.GetComponent<Text>().text = FirebaseAuthentication.instance.DisplayName();
    }
    #endregion
}
