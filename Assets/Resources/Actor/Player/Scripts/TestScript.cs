using UnityEngine;

public class TestScript : MonoBehaviour
{
    private PlayerInput input;
    private Animator animator;
    private bool attacking = false;

    private void Awake()
    {
        input = new();
        input.Player.Enable();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var current = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log($"State: {current.shortNameHash}  normalizedTime: {current.normalizedTime}");

        if (attacking)
        {
            AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);
            if (animatorState.normalizedTime >= 1)
            {
                attacking = false;
                Debug.Log("stop attacking");
                animator.Play("Player_idle");
            }
        }

        if (input.Player.Attack.triggered && !attacking) {
            animator.Play("Player_attack");
            attacking = true;
            return;
        }
    }
}
