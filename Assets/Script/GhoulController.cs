using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class GhoulController : MonoBehaviour
{
    Animator animator;

    InputAction moveInputAction;

    [SerializeField]
    float speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        moveInputAction = InputSystem.actions.FindAction("Move");
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
            Vector3 horizon = (horizontalX + horizontalZ) * speed * Time.deltaTime;
            transform.Translate(horizon);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
