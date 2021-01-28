using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]

public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _initialTailSize;
    [SerializeField] private float _speed;    
    [SerializeField] private float _dragSpeed;
    [SerializeField] private float _tailSpringiness;
    [SerializeField] private Vector2 _snakeBorders;

    private SnakeInput _input;
    private List<Segment> _tail;
    private TailGenerator _tailGenerator;

    public UnityAction<int> SizeUpdated;

    private void Awake()
    {
        _input = GetComponent<SnakeInput>();
        _tailGenerator = GetComponent<TailGenerator>();
        _tail = _tailGenerator.Generate(_initialTailSize);
        SizeUpdated?.Invoke(_tail.Count);
    }

    private void FixedUpdate()
    {
        Move();        
    }

    private void Move()
    {
        Vector3 previousPosition = _head.transform.position;        
        float delta = _input.DeltaX;

        if (_head.transform.position.x <= _snakeBorders.x && delta < 0)
            delta = 0;

        if (_head.transform.position.x >= _snakeBorders.y && delta > 0)
            delta = 0;

        Debug.Log("delta is:" + delta);
        _head.transform.up = Vector3.up + Vector3.right * delta / 10 / _dragSpeed;
        
        Vector3 nextPosition = Vector3.zero;
        nextPosition.x = (_head.transform.up * _dragSpeed * Time.fixedDeltaTime).x;
        nextPosition.y += _speed * Time.fixedDeltaTime;
        nextPosition += previousPosition;
        for (int i = 0; i < _tail.Count; i++)
        {
            Segment segment = _tail[i];

            Vector3 tempPosition = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _tailSpringiness * Time.deltaTime);
            previousPosition = tempPosition;
        }

        _head.Move(nextPosition);
    }

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;
    }

    private void OnDisable()
    {        
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;
    }

    private void OnBlockCollided()
    {
        if (_tail.Count > 0)
        {
            Segment deletedSegment = _tail[_tail.Count - 1];
            _tail.Remove(deletedSegment);
            Destroy(deletedSegment.gameObject);
            SizeUpdated?.Invoke(_tail.Count);
        }
    }

    private void OnBonusCollected(int bonusSize)
    {
        _tail.AddRange(_tailGenerator.Generate(bonusSize));
        SizeUpdated?.Invoke(_tail.Count);
    }
}
