using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParticlesDisplayer : MonoBehaviour 
{
	public RectTransform imageTransform;
    RawImage image;
    float duration;

    private void Awake()
    {
        image = imageTransform.gameObject.GetComponent<RawImage>();
    }

    private void Start()
    {
        StateManager.stateChangedDelegate += onStateChanged;
    }

    public void setDuration(float dur)
    {
        duration = dur;
    }

    public void ResetPosition()
	{
		imageTransform.anchoredPosition = Vector2.zero;
	}

	public void MoveToPosition(Vector3 pos)
	{
        setSolidImage();
        StopCoroutine("waitUntilEfxFinish");
        StartCoroutine("waitUntilEfxFinish");
        imageTransform.position = pos;
	}

    IEnumerator waitUntilEfxFinish()
    {
        yield return new WaitForSeconds(duration);
        setTransparentImage();
    }

    void onStateChanged()
    {
        if (!StateManager.instance.isInGame())
        {
            setTransparentImage();
        } else
        {
            setSolidImage();
        }
    }

    public void setSolidImage()
    {
        image.color = Color.white;
    }

    public void setTransparentImage()
    {
        image.color = Color.clear;
    }
}
