using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; 

public class FirebaseLoader : MonoBehaviour {

    [SerializeField]
    GameObject Authentication;

    [SerializeField]
    GameObject Database;

    private void Awake()
    {
		InstantiateFirebaseModules();
    }
		
    void InstantiateFirebaseModules()
    {
        if (FirebaseAuthentication.instance == null)
        {
            Instantiate(Authentication);
        }

		if (FirebaseDB.instance == null)
        {
            Instantiate(Database);
        }
    }

}
