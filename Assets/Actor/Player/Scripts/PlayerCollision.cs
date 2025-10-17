using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerState playerState;
    private AnimationController animationController;

    [SerializeField] private int _frameCount = 0;

    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
        animationController = GetComponent<AnimationController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _frameCount = 0;
        if (other.CompareTag("small_enemy") && !playerState.IsAttacking)
        {
            playerState.Damage();
            animationController.StartAnimation("Player_damage");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _frameCount++;
        if (other.CompareTag("small_enemy"))
        {
            if (playerState.IsAttacking)
            {
                playerState.StopDamage();
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("small_enemy"))
        {
            playerState.StopDamage();
            animationController.SetNextAnimation("Player_idle");
        }
    }
}
