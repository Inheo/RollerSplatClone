using System;
using UnityEngine;

public interface ISwipeInputService : IInputService
{
    float Sensetive { get; set; }
    bool ContinuousDetection { get; set; }

    event Action onSwipeCancelled;
}

public class SwipeInputService : InputService, ISwipeInputService
{
    [SerializeField] private float _sensetivity = 10;
    [SerializeField] private bool _continuousDetection = false;

    private bool _waitingForSwipe = false;
    private float _minMoveDistance = 0.1f;
    private Vector3 _startPosition;
    private Vector3 _offset;
    private Vector2 _axis;

    public float Sensetive { get => _sensetivity; set => _sensetivity = value; }
    public bool ContinuousDetection { get => _continuousDetection; set => _continuousDetection = value; }
    
    public override Vector2 Axis => _axis;

    public override event Action<Vector2> onChangedDirection;
    public event Action onSwipeCancelled;

    private void Start()
    {
        UpdateSensetivity();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            StartSwipe();
        }

        if (_waitingForSwipe == true && Input.GetMouseButton(0) == true)
        {
            TrysSwipe();
        }

        if (_continuousDetection == false)
        {
            TryCancelingSwipe();
        }
    }
    private void UpdateSensetivity()
    {
        int screenShortSide = Screen.width < Screen.height ? Screen.width : Screen.height;
        _minMoveDistance = screenShortSide / _sensetivity;
    }

    private void TryCancelingSwipe()
    {
        if (Input.GetMouseButtonUp(0) == true)
        {
            if (_waitingForSwipe == true)
            {
                onSwipeCancelled?.Invoke();
            }
        }
    }

    private void TrysSwipe()
    {
        _offset = Input.mousePosition - _startPosition;

        if (_offset.magnitude >= _minMoveDistance)
        {
            _axis = DirectionDetection(_offset);
            onChangedDirection?.Invoke(_axis);

            if (_continuousDetection == false)
                Reset();
            else
                InitialPositions();
        }
    }

    private Vector2 DirectionDetection(Vector3 vector)
    {
        if (Math.Abs(vector.x) > Mathf.Abs(vector.y))
        {
            return vector.x > 0 ? Vector2.right : Vector2.left;
        }

        return vector.y > 0 ? Vector2.up : Vector2.down;
    }

    private void Reset()
    {
        InitialPositions();
        _waitingForSwipe = false;
    }

    private void StartSwipe()
    {
        InitialPositions();
        _waitingForSwipe = true;
    }

    private void InitialPositions()
    {
        _startPosition = Input.mousePosition;
        _offset = Vector3.zero;
    }
}
