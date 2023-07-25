using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class TweenScaleUI : MonoBehaviour
    {
        [SerializeField] private Vector3 initialScale;
        [SerializeField] private Vector3 finalScale;

        [SerializeField] private Ease ease;
        [SerializeField] private float tweenTime;

        [SerializeField] private UnityEvent onInitialScaleComplete;
        [SerializeField] private UnityEvent onFinalScaleComplete;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _rectTransform.localScale = initialScale;
        }

        public void DoInitialScale()
        {
            _rectTransform.DOScale(initialScale, tweenTime).SetEase(ease).OnComplete(() =>  onInitialScaleComplete?.Invoke());
        }

        public void DoInitialScaleAfterDelay(float delay)
        {
            _rectTransform.DOScale(initialScale, tweenTime).SetEase(ease).SetDelay(delay).OnComplete(() => onInitialScaleComplete?.Invoke());
        }

        public void DoFinalScale()
        {
            _rectTransform.DOScale(finalScale, tweenTime).SetEase(ease).OnComplete(() => onFinalScaleComplete?.Invoke());
        }

        public void DoFinalScaleAfterDelay(float delay)
        {
            _rectTransform.DOScale(finalScale, tweenTime).SetEase(ease).SetDelay(delay).OnComplete(() => onFinalScaleComplete?.Invoke());
        }
    }
}
