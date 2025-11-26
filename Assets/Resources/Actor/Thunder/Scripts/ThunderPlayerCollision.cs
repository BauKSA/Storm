using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThunderPlayerCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("player")) return;

        ThunderState state = GetComponent<ThunderState>();
        if (state.ChangingCollider) return;
        if (!state.Damage) return;

        PlayerState playerState = other.GetComponent<PlayerState>();
        AnimationController playerAnimation = other.GetComponent<AnimationController>();

        playerState.Death();

        if (!playerState || !playerAnimation)
        {
            Debug.Log("No Player State or Animation");
            return;
        }

        state.NoDamage();

        Game.Instance.Health -= Game.Instance.ThunderDamage;
    }
}