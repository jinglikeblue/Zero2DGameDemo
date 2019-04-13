using UnityEngine;
using DG.Tweening;
using System;

public class WindowTween : MonoBehaviour {

    [Header("缓动对象")]
    public Transform tweenTarget;

    public Vector3 startScale = Vector3.one / 2;
    public Vector3 endScale = Vector3.one * 1.5f;
    public float tweenInTime = 0.5f;
    public float tweenOutTime = 0.5f;

    void Start () {
		if(tweenTarget.GetComponent<CanvasGroup>() == null)
        {
            tweenTarget.gameObject.AddComponent<CanvasGroup>();
        }
	}	
	
	void Update () {
		
	}

    public void Show()
    {
        Show(null);
    }

    public void Show(TweenCallback onOver)
    {
        gameObject.SetActive(true);
        TweenIn(onOver);
    }

    public void Hide()
    {
        TweenOut(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void TweenIn(TweenCallback onOver = null)
    {
        tweenTarget.localScale = startScale;        
        tweenTarget.DOScale(Vector3.one, tweenInTime).SetEase(Ease.OutBack).OnComplete(onOver);
        tweenTarget.GetComponent<CanvasGroup>().alpha = 0;
        tweenTarget.GetComponent<CanvasGroup>().DOFade(1, tweenInTime);
    }

    public void TweenOut(TweenCallback onOver = null)
    {
        tweenTarget.DOScale(endScale, tweenOutTime).SetEase(Ease.InBack).OnComplete(onOver);        
        tweenTarget.GetComponent<CanvasGroup>().DOFade(0, tweenOutTime);
    }
}
