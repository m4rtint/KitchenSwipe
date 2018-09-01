using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

public class FirebaseLoader : MonoBehaviour {

    [SerializeField]
    GameObject authentication;

    [SerializeField]
    GameObject database;

    private void Awake()
    {
		instantiateFirebaseModules();
    }
		
    void instantiateFirebaseModules()
    {
        if (FirebaseAuthentication.instance == null)
        {
            Instantiate(authentication);
        }

		if (FirebaseDB.instance == null)
        {
            Instantiate(database);
        }
    }

}
