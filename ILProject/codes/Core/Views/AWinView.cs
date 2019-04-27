using UnityEngine;
using DG.Tweening;
using System;
using IL.Zero;

namespace IL
{
    public class AWinView : AView
    {
        private Action _onComplete = null;

        public void DefaultShowEffect(Action onComplete = null)
        {            
            CanvasGroup cg = AudoGetComponent<CanvasGroup>();

            gameObject.transform.localScale = Vector3.zero;
            cg.alpha = 0;
            
            var seq = DOTween.Sequence();
            seq.Append(gameObject.transform.DOScale(1.05f, 0.2f));            
            seq.Append(gameObject.transform.DOScale(0.98f, 0.2f));
            seq.Append(gameObject.transform.DOScale(1f, 0.2f));
            seq.Insert(0.1f, cg.DOFade(1, 0.7f));
            seq.AppendCallback(() =>
            {
                onComplete?.Invoke();                
            });
        }

        
        public void DefaultCloseEffect(Action onComplete = null)
        {            
            if (false == IsDestroyed)
            {                
                _onComplete = onComplete;
                CanvasGroup cg = AudoGetComponent<CanvasGroup>();            
                cg.DOFade(0, 0.4f);            
                gameObject.transform.DOScale(Vector3.one * 0.5f, 0.4f).OnComplete(OnComplete);
            }
        }

        private void OnComplete()
        {
            _onComplete?.Invoke();
            Destroy();
            _onComplete = null;            
        }
    }



}