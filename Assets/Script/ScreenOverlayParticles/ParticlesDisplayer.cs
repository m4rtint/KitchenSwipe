using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParticlesDisplayer : MonoBehaviour 
{
    [SerializeField]
	RectTransform imageTransform;
    RawImage rawImage;
    float duration;

    private void Awake()
    {
        rawImage = imageTransform.gameObject.GetComponent<RawImage>();
        setTransparentImage();
    }

    private void Start()
    {
        StateManager.stateChangedDelegate += onStateChanged;
    }

    private void OnDestroy()
    {
        StateManager.stateChangedDelegate -= onStateChanged;
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
        rawImage.color = Color.white;
    }

    public void setTransparentImage()
    {
        rawImage.color = Color.clear;
    }
}
