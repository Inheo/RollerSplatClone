using UnityEngine;

public interface IInput
{
    Vector3 Direction { get; }
    event System.Action<Vector3> onChangedDirection;
}
