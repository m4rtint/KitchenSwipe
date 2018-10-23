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
        StopCoroutine("waitUntilEfxFin");
        StartCoroutine("waitUntilEfxFin");
        imageTransform.position = pos;
	}

    IEnumerator waitUntilEfxFin()
    {
        yield return new WaitForSeconds(duration);
        setTransparentImage();
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
