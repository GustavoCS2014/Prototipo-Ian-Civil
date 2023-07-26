using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Flexible
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class FlexibleUIButton : FlexibleUI
    {
        [SerializeField] private ButtonType buttonType;
        [SerializeField] private Image icon;

        private Image _image;
        private Button _button;

        protected override void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();

            base.Awake();
        }

        protected override void OnSkinUI()
        {
            _button.targetGraphic = _image;

            _image.sprite = skinData.ButtonSprite;
            _button.spriteState = skinData.ButtonSpriteState;

            switch (buttonType)
            {
                case ButtonType.Default:
                    _image.color = skinData.DefaultColor;
                    icon.sprite = skinData.DefaultIcon;
                    break;
                case ButtonType.Confirm:
                    _image.color = skinData.ConfirmColor;
                    icon.sprite = skinData.ConfirmIcon;
                    break;
                case ButtonType.Decline:
                    _image.color = skinData.DeclineColor;
                    icon.sprite = skinData.DeclineIcon;
                    break;
                case ButtonType.Warning:
                    _image.color = skinData.WarningColor;
                    icon.sprite = skinData.WarningIcon;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType));
            }

            base.OnSkinUI();
        }
    }

    public enum ButtonType
    {
        Default,
        Confirm,
        Decline,
        Warning
    }
}
