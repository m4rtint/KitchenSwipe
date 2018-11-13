using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class TrashInput : MonoBehaviour
{
    public delegate void TrashInputDelegate();
    public TrashInputDelegate animateDoubleTapDelegate;
    public TrashInputDelegate doubleTapDelegate;

    [SerializeField]
    UserInput userInput;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            tapped();
        });
    }

    public void tapped()
    {
        if (userInput.CanSwipe())
        {
            animateDoubleTapDelegate();
            userInput.disableSwipe();
        }
    }

    public void runDoubleTapDelegate()
    {
        doubleTapDelegate();
        userInput.enableSwipe();
    }
}
