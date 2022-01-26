using UnityEngine;

public interface IMove
{
    Rigidbody Rigidbody { get; }
    float MoveSpeed { get; }

    void ProcessMovement();
}
