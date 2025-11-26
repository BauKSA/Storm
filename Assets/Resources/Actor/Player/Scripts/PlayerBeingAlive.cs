using UnityEngine;

public class PlayerBeingAlive : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
        if (!sprRenderer) return;

        sprRenderer.sortingLayerName = "Player";
    }
}
