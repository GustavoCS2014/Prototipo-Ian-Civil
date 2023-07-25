using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class PopUpMessageUI : MonoBehaviour
    {
        [SerializeField] private Ease ease;
        [SerializeField] private float tweenTime;
        [SerializeField] private string message;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private UnityEvent onHide;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            text.text = message;

            _rectTransform.localScale = new Vector3(1f, 0f, 1f);
        }

        public void Show()
        {
            _rectTransform.DOScaleY(1f, tweenTime).SetEase(ease).onComplete += Hide;
        }

        public void Hide()
        {
            _rectTransform.DOScaleY(0f, tweenTime).SetEase(ease).SetDelay(2f).onComplete += () => onHide?.Invoke();
        }
    }
}
