using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PipeButton : MonoBehaviour, IPointerClickHandler
{
    public Pipe AssociatedPipe;

    [SerializeField] private float _rotationSpeed = 180f;
    private float _zAngle;

    private void Awake()
    {
        SetZ(_zAngle = AssociatedPipe.RotationsCount * -90);
        Shuffle();
    }

    public void Shuffle()
    {
        if (AssociatedPipe.Fixed) return;
        var random = Random.Range(0, 4);
        for (int i = 0; i < random; i++) AssociatedPipe.RotateClockwise();
        
        SetZ(_zAngle = AssociatedPipe.RotationsCount * -90);
    }

    private void Update()
    {
        if (transform.localRotation.z == _zAngle) return;
        SetZ(Mathf.MoveTowardsAngle(GetZ(), _zAngle, _rotationSpeed * Time.deltaTime));
    }

    [ExecuteInEditMode]
    public void Init(Pipe pipe)
    {
        AssociatedPipe = pipe;

        var image = GetComponent<Image>();
        if (pipe.PipeSprite == null)
            image.enabled = false;
        else
            image.sprite = pipe.PipeSprite;

        SetZ(AssociatedPipe.RotationsCount * -90);
    }

    public void RotateClockwise()
    {
        AssociatedPipe.RotateClockwise();
        _zAngle -= 90;
    }
    public void RotateCounterClockwise()
    {
        AssociatedPipe.RotateCounterClockwise();
        _zAngle += 90;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (AssociatedPipe.Fixed) return;
		if (eventData.button == PointerEventData.InputButton.Left)
			RotateClockwise();
		else if (eventData.button == PointerEventData.InputButton.Right)
			RotateCounterClockwise();
    }

    private void SetZ(float angle)
    {
        var currentAngles = transform.localRotation.eulerAngles;
        currentAngles.z = angle;
        transform.localRotation = Quaternion.Euler(currentAngles);
    }
    private float GetZ() => transform.localRotation.eulerAngles.z;
}
