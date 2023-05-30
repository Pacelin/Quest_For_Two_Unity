using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : InputAction
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _switchedSprite;
    [SerializeField] private Image _image;

    public override void ApplyAction()
    {
        if (_image.sprite == _defaultSprite)
            _image.sprite = _switchedSprite;
        else
            _image.sprite = _defaultSprite;
    }
}