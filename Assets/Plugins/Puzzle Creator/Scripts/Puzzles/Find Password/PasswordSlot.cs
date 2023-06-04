using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PasswordSlot : MonoBehaviour, IPointerClickHandler
{
	public event System.Action<int> OnValueChanged;
	public bool IsCorrect => _currentSpriteIndex == _correctSpriteIndex;

	[SerializeField] private Image _selfImage;
	[SerializeField] private Sprite[] _sprites;
	[SerializeField] private int _correctSpriteIndex;
	[SerializeField] private int _valueIndexOnAwake;

	private int _currentSpriteIndex = 0;

	public void Reset()
	{
		_selfImage.sprite = _sprites[_correctSpriteIndex];
	}

	private void Awake()
	{
		ChangeValue(_valueIndexOnAwake);
	}

	public void MoveNext() =>
		ChangeValue((_currentSpriteIndex + 1) % _sprites.Length);

	public void MovePrevious() =>
		ChangeValue((_currentSpriteIndex + _sprites.Length - 1) % _sprites.Length);

	private void ChangeValue(int newValue)
	{
		_currentSpriteIndex = newValue;
		_selfImage.sprite = _sprites[_currentSpriteIndex];
		OnValueChanged?.Invoke(newValue);
	}

    public void OnPointerClick(PointerEventData eventData)
    {
		if (eventData.button == PointerEventData.InputButton.Left)
			MoveNext();
		else if (eventData.button == PointerEventData.InputButton.Right)
			MovePrevious();
    }
}
