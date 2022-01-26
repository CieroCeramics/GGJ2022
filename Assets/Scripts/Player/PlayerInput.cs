using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : MonoBehaviour, IMove
{
    //Properties
    //====================================================================================================================//

    [SerializeField, Header("Inputs")] private KeyCode forwardInput;
    [SerializeField] private KeyCode backwardInput;
    [SerializeField] private KeyCode leftInput;
    [SerializeField] private KeyCode rightInput;

    public Rigidbody Rigidbody
    {
        get
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
            
            return _rigidbody;
        }
    }
    private Rigidbody _rigidbody;

    public float MoveSpeed => moveSpeed;
    [SerializeField, Header("Speed")] private float moveSpeed = 5;

    //IMove Functions
    //====================================================================================================================//

    public void ProcessMovement()
    {
        var currentVelocity = Rigidbody.velocity;
        Quaternion targetRotation = transform.rotation;

        //Up movement
        if (Input.GetKey(forwardInput))
        {
            currentVelocity.z = moveSpeed;
            targetRotation = Quaternion.identity;
        }
        //Down movement
        else if (Input.GetKey(backwardInput))
        {
            currentVelocity.z = -moveSpeed;
            targetRotation = Quaternion.Euler(0, 180, 0);
        }

//Left movement
        if (Input.GetKey(leftInput))
        {
            currentVelocity.x = -moveSpeed;
            targetRotation = Quaternion.Euler(0, 270, 0);
        }
        //Right movement
        else if (Input.GetKey(rightInput))
        {
            currentVelocity.x = moveSpeed;
            targetRotation = Quaternion.Euler(0, 90, 0);
        }

        Rigidbody.velocity = currentVelocity;
        transform.rotation = targetRotation;
    }

    //====================================================================================================================//
}
