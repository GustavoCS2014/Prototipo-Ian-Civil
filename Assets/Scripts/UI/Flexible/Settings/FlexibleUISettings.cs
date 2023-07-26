using UnityEngine;
using UnityEngine.UI;

namespace UI.Flexible.Settings
{
    [CreateAssetMenu(menuName = "Flexible UI Data")]
    public class FlexibleUISettings : ScriptableObject
    {
        [SerializeField] private Sprite buttonSprite;
        public Sprite ButtonSprite => buttonSprite;
        [SerializeField] private SpriteState buttonSpriteState;
        public SpriteState ButtonSpriteState => buttonSpriteState;

        [SerializeField] private Color defaultColor;
        public Color DefaultColor => defaultColor;
        [SerializeField] private Sprite defaultIcon;
        public Sprite DefaultIcon => defaultIcon;

        [SerializeField] private Color confirmColor;
        public Color ConfirmColor => confirmColor;
        [SerializeField] private Sprite confirmIcon;
        public Sprite ConfirmIcon => confirmIcon;

        [SerializeField] private Color declineColor;
        public Color DeclineColor => declineColor;
        [SerializeField] private Sprite declineIcon;
        public Sprite DeclineIcon => declineIcon;

        [SerializeField] private Color warningColor;
        public Color WarningColor => warningColor;

        [SerializeField] private Sprite warningIcon;
        public Sprite WarningIcon => warningIcon;
    }
}
