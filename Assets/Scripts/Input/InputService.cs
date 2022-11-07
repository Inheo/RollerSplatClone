using System;
using UnityEngine;

public abstract class InputService : MonoBehaviour, IInputService
{
    public abstract Vector2 Axis { get; }

    public abstract event Action<Vector2> onChangedDirection;
}