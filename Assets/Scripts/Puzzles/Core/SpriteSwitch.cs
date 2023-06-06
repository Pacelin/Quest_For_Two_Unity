using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitch : MonoBehaviour
{
    [field: SerializeField] public int SpriteIndex { get; private set; }
    
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;

    private void Awake() =>
        _image.sprite = _sprites[SpriteIndex];
    
    public void MoveNext()
    {
        SpriteIndex = (SpriteIndex + 1) % _sprites.Length;
        _image.sprite = _sprites[SpriteIndex];
    }

    public void MovePrevious()
    {
        SpriteIndex = (_sprites.Length + SpriteIndex - 1) % _sprites.Length;
        _image.sprite = _sprites[SpriteIndex];
    }
}
