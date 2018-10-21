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
        iTween.ScaleTo(gameObject, Vector3.one, 0.5f);
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
        iTween.ScaleTo(gameObject, Vector3.zero, 0.5f);
    }
}
