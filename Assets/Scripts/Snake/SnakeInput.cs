using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    [SerializeField] private float _deadRadius;
    private Vector2 _startTouch;
    private Vector2 _previousPosition;
    private Vector2 _currentPosition;

    private Camera _camera;

    public float DeltaX => Mathf.Abs(_currentPosition.x - _startTouch.x)>_deadRadius? _currentPosition.x - _startTouch.x : 0;

    private void Start()
    {
        _camera = Camera.main;
    }

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
        #region Mobile Input
        //if (Input.touchCount>0)
        //{
        //    _previousPosition = _currentPosition;
        //    _currentPosition = Input.GetTouch(0).position;
        //    if (Input.GetTouch(0).phase == TouchPhase.Began)
        //    {                                
        //        _startTouch = Input.GetTouch(0).position;
        //    }
        //    else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
        //    {
        //        _startTouch = Vector2.zero;
        //        _currentPosition = Vector2.zero;
        //        _previousPosition = Vector2.zero;
        //    }
        //}
        #endregion        

        if (_previousPosition == _currentPosition)
            _startTouch = _currentPosition;
    }
}
