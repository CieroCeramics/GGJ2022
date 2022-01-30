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
    private Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
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
         //  animator.SetBool("Walking", true);
        }
        //Down movement
        else if (Input.GetKey(backwardInput))
        {
            currentVelocity.z = -moveSpeed;
            targetRotation = Quaternion.Euler(0, 180, 0);
           // animator.SetBool("Walking", true);
        }

//Left movement
        if (Input.GetKey(leftInput))
        {
            currentVelocity.x = -moveSpeed;
            targetRotation = Quaternion.Euler(0, 270, 0);
            //animator.SetBool("Walking", true);
        }

        //Right movement
        else if (Input.GetKey(rightInput))
        {
            currentVelocity.x = moveSpeed;
            targetRotation = Quaternion.Euler(0, 90, 0);
            //animator.SetBool("Walking", true);
        }




       if (Input.GetKeyDown(forwardInput))
        {
            animator.SetBool("Walking", true);
        }
        //Down movement
        else if (Input.GetKeyDown(backwardInput))
        {

            animator.SetBool("Walking", true);
        }
         if (Input.GetKeyDown(leftInput))
        {

            animator.SetBool("Walking", true);
        }
        if (Input.GetKeyDown(rightInput))
        {
            animator.SetBool("Walking", true);
        }
        else if (Input.GetKeyUp(rightInput) || Input.GetKeyUp(leftInput) || Input.GetKeyUp(forwardInput)|| Input.GetKeyUp(backwardInput) )
        {

            animator.SetBool("Walking", false);

           // Debug.Log("stop");
        }




        Rigidbody.velocity = currentVelocity;
        transform.rotation = targetRotation;
    }

    //====================================================================================================================//
}
