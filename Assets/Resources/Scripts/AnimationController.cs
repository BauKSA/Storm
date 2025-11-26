using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private string lastAnimation = "";
    private string currentAnimation = "";
    //private readonly string DefaultAnimation = "Player_idle";

    private bool endAfterLooped = false;
    private bool animationReadyToEnd = false;
    private bool newlyInitialized = false;

    private Action OnEndAnimation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(!animator) Debug.LogError("Animator component not found in AnimationController");
    }

    private void Update()
    {
        if (!animator) return;

        if (newlyInitialized)
        {
            newlyInitialized = false;
            return;
        }

        if (animationReadyToEnd)
        {
            animationReadyToEnd = false;
            EndAnimation();
            return;
        }

        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);

        if (endAfterLooped)
        {
            if (animatorState.normalizedTime >= 1)
            {
                endAfterLooped = false;
                animationReadyToEnd = true;
            }
        }
    }

    //Ejecuta la animación directo, sin importar cuál sea la actual ni su estado
    public void StartAnimation(string animation, bool singleLoop = false, Action Callback = null)
    {
        if (currentAnimation == animation) return;

        lastAnimation = currentAnimation;
        currentAnimation = animation;

        endAfterLooped = singleLoop;

        OnEndAnimation = Callback;

        animator.Play(animation, 0, 0f);
        newlyInitialized = true;
    }

    public void EndAnimation()
    {
        (currentAnimation, lastAnimation) = (lastAnimation, currentAnimation);

        OnEndAnimation?.Invoke();
        OnEndAnimation = null;

        if (!animator.isActiveAndEnabled) return;
        animator.Play(currentAnimation);
    }

    //Guarda la animación en cola para ejecutarla al terminar la actual
    public void SetNextAnimation(string animation)
    {
        if (endAfterLooped) lastAnimation = animation;
        else StartAnimation(animation);
    }
}
