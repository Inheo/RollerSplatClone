using UnityEngine;

public interface IInputService
{
    Vector2 Axis { get; }
    event System.Action<Vector2> onChangedDirection;
}
