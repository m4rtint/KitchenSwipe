using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    //Delegate
    public delegate void TransitionDelegate();
    public TransitionDelegate completedTransition;
    
    public static TransitionManager instance = null;
    readonly Vector3 expanded = new Vector3(100, 100, 0);

    #region mono
    private void Awake()
    {
        instance = this;
        transform.localScale = Vector3.zero;
        onSceneLoaded();
    }

    void onSceneLoaded()
    {
        if (StateManager.instance.isPreparingGame())
        {
            onGameSceneStart();
        }
    }


    public void startInfiniteGameScene()
    {
        onSceneChange("changeSceneToGame");
    }


    public void startMainMenuScene()
    {
        onSceneChange("changeSceneToMenu");
    }

    public void startTutorialScene() {
        StateManager.instance.prepareGame();
        SceneManager.LoadScene("TutorialMode");
    }

    #endregion


    #region AnimateToScene
    void onSceneChange(string methodName)
    {
        Hashtable ht = new Hashtable();
        ht.Add("scale", expanded);
        ht.Add("time", 2);
        ht.Add("oncomplete", methodName);
        iTween.ScaleTo(gameObject, ht);
    }

    void changeSceneToGame()
    {
        StateManager.instance.prepareGame();
        SceneManager.LoadScene("InfiniteGameMode");
    }

    void changeSceneToMenu()
    {
        StateManager.instance.setToMenu();
        SceneManager.LoadScene(0);
    }
    #endregion

    #region AnimateStartGame
    void onGameSceneStart()
    {
        transform.localScale = expanded;
        Hashtable ht = new Hashtable();
        ht.Add("scale", Vector3.zero);
        ht.Add("time", 1.0f);
        ht.Add("oncomplete", "callCompletedTransitionDelegate");
        iTween.ScaleTo(gameObject, ht);
    }


    void callCompletedTransitionDelegate()
    {
        completedTransition();
    }
    #endregion
   
}
