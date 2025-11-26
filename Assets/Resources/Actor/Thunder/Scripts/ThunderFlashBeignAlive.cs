using UnityEngine;

public class ThunderFlashBeignAlive : MonoBehaviour
{
    private float flashDuration = 100f;
    private float flashTimer = 0f;

    private void Start()
    {
        SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
        if (!sprRenderer) return;

        sprRenderer.sortingLayerName = "Thunder";
    }

    public void SetFlashDuration(float duration)
    {
        if (flashDuration != 100f) return;
        flashDuration = duration;
    }

    void Update()
    {
        flashTimer += Time.deltaTime;
        if(flashTimer >= flashDuration)
        {
            Destroy(gameObject);
        }
    }
}
