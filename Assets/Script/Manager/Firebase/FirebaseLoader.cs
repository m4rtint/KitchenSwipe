using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseLoader : MonoBehaviour {

    [SerializeField]
    GameObject Authentication;

    private void Awake()
    {
        if(FirebaseAuthentication.instance == null)
        {
            Instantiate(Authentication);
        }
    }

}
