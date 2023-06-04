using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FindPasswordPuzzle : MonoBehaviour
{
    public UnityEvent OnSolve;

	[SerializeField] private PasswordSlot[] _slots;

	private void Awake()
	{
		_slots = GetComponentsInChildren<PasswordSlot>();
	}

	private void OnEnable()
	{
		foreach (var slot in _slots)
			slot.OnValueChanged += OnSlotValueChanged;
	}

	private void OnDisable()
	{
		foreach (var slot in _slots)
			slot.OnValueChanged -= OnSlotValueChanged;
	}

	private void OnSlotValueChanged(int newValue)
	{
		if (_slots.All(slot => slot.IsCorrect))
			OnSolve?.Invoke();
	}
}