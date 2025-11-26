using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public GameObject drop = null;

    private float spawnTimer = 0f;

    private Vector2 xRange = new(-1.5f, 1.5f);

    private void Spawn()
    {
        float x = Random.Range(xRange.x, xRange.y);
        float y = 1f;

        Vector3 spawnPos = new(x, y, 0);
        Quaternion rot = Quaternion.Euler(0, 0, 0);

        if (!drop) return;
        GameObject dropObject = Instantiate(drop, spawnPos, rot);

        dropObject.transform.localScale = Vector3.one;
        SpriteRenderer sprRenderer = dropObject.GetComponent<SpriteRenderer>();
        if (!sprRenderer) return;

        sprRenderer.sortingLayerName = "Ant";
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= Game.Instance.DropSpawnDelay)
        {
            spawnTimer -= Game.Instance.DropSpawnDelay;
            Spawn();
        }
    }
}
