using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    [SerializeField] private float _deadRadius;
    private Vector2 _startTouch;
    private Vector2 _previousPosition;
    private Vector2 _currentPosition;

    public float DeltaX => Mathf.Abs(_currentPosition.x - _startTouch.x)>_deadRadius? _currentPosition.x - _startTouch.x : 0;

    private void Update()
    {
        #region Standalone Input
        if (Input.GetMouseButton(0))
        {
            _previousPosition = _currentPosition;
            _currentPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _startTouch = Vector2.zero;
            _currentPosition = Vector2.zero;
            _previousPosition = Vector2.zero;
        }
        #endregion

        if (_previousPosition == _currentPosition)
            _startTouch = _currentPosition;
    }
}
