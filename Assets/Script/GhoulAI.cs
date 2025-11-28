using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GhoulAI : MonoBehaviour
{
    Animator animator;

    // Awake is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayerVisibility(bool visible)
    {
        animator.SetBool("PlayerIsVisible", visible);
    }
}
