using UnityEngine;

public class LocationTransfer : MonoBehaviour
{
    [SerializeField] private Location _nextLocation;
    [SerializeField] private Location _currentLocation;
    [Space]
    [SerializeField] private float _fadeOutDelay = 0;
    [SerializeField] private float _fadeOutTime = 0.4f;
    [Space]
    [SerializeField] private float _fadeInDelay = 0.2f;
    [SerializeField] private float _fadeInTime = 0.3f;

    public void ApplyTransfer()
    {
        StartCoroutine(_currentLocation.Hide(_fadeOutDelay, _fadeOutTime));
        StartCoroutine(_nextLocation.Show(_fadeInDelay, _fadeInTime));
    }
}