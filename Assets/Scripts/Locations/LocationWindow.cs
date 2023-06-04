using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class LocationWindow : MonoBehaviour
{
    [SerializeField] private float _inventoryPixelSize;

    private Camera _mainCamera;
    private Canvas _selfCanvas;


    private void Awake()
    {
        _mainCamera = Camera.main;
        _selfCanvas = GetComponent<Canvas>();

        //transform.position = new Vector3(0, , 0);
    }

    private void Fit()
    {
        var fullHeight = _mainCamera.orthographicSize * 2f;
        var fullWidth = fullHeight * Screen.width / Screen.height;
        transform.localScale = new Vector3();
    }
}
