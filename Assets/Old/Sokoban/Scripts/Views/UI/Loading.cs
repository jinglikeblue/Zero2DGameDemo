using System;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 场景切换界面
/// </summary>
public class Loading : MonoBehaviour {

    public CanvasGroup cg;
    
	void Start () {
    }
	
	
	void Update () {
		
	}

    public void Show(TweenCallback onShow, TweenCallback onHide = null)
    {
        gameObject.SetActive(true);
        cg.alpha = 0;
        var sq = DOTween.Sequence();
        sq.Append(cg.DOFade(1, 1f));
        sq.AppendCallback(onShow);
        //sq.AppendInterval(1f);
        sq.Append(cg.DOFade(0, 1f));
        sq.AppendCallback(onHide);
        sq.AppendCallback(() =>
        {
            gameObject.SetActive(false);
        });
        sq.Play();
    }
}
