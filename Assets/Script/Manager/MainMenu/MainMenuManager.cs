using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(QuickApplication);
    }

    private void QuickApplication()
    {
        Application.Quit();
    }
}
