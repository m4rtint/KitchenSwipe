using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialConfirmation : MonoBehaviour {

    [SerializeField]
    Button goTutorial;

    [SerializeField]
    Button skipTutorial;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        setupButton();
    }

    public void showTutorialConfirmation() {
        iTween.ScaleTo(gameObject, Vector3.one, 1f);
    } 

    void setupButton() {
        goTutorial.onClick.AddListener(delegate {
            TransitionManager.instance.startTutorialScene();
        });

        skipTutorial.onClick.AddListener(delegate
        {
            hideTutorialConfirmPanel();
        });
    }

    private void hideTutorialConfirmPanel() {
        Hashtable ht = new Hashtable();
        ht.Add("scale", Vector3.zero);
        ht.Add("oncomplete", "onHidePanel");
        ht.Add("time", 1f);
        iTween.ScaleTo(gameObject, ht);
    }

    private void onHidePanel() {
        Destroy(gameObject);
    }
}
