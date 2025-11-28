using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class GhoulController : MonoBehaviour
{
    Animator animator;

    Rigidbody rb;

    InputAction moveInputAction;
    InputAction jumpInputAction;

    [SerializeField]
    float speed = 1;

    bool isGrounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        moveInputAction = InputSystem.actions.FindAction("Move");
        jumpInputAction = InputSystem.actions.FindAction("Jump");

        jumpInputAction.performed += JumpInputAction_performed;
    }

    private void JumpInputAction_performed(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            rb.AddForce(0, 10000, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = moveInputAction.ReadValue<Vector2>();
        float inputHorizontalX = move.x;
        float inputHorizontalZ = move.y;

        if (inputHorizontalX != 0 || inputHorizontalZ != 0)
        {
            animator.SetBool("IsMoving", true);

            Vector3 horizontalX = inputHorizontalX * Vector3.right;
            Vector3 horizontalZ = inputHorizontalZ * Vector3.forward;
            if (rb == null)
            {
                Vector3 horizon = (horizontalX + horizontalZ) * speed * Time.deltaTime;
                transform.Translate(horizon);
            }
            else
            {
                rb.linearVelocity = (horizontalX + horizontalZ) * speed;
            }
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
