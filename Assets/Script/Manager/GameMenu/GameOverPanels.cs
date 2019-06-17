using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanels : MonoBehaviour
{
    readonly Color babyBlue = new Color(193f/255f, 215f/255f, 230f/255f);

    [SerializeField]
    RectTransform panel;
    public Button tab;
    [SerializeField]
    Text tabText;

    #region Panels
    public void activatePanel(bool show)
    {
        if (show)
        {
            transform.SetAsLastSibling();
            tabText.color = Color.white;
            tab.GetComponent<Image>().color = babyBlue;
        }
        else
        {
            tabText.color = babyBlue;
            tab.GetComponent<Image>().color = Color.white;
        }
    }
    #endregion
}
