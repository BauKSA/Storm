using UnityEngine;

public class DropSpwan : MonoBehaviour
{
    static public DropSpwan Instance { get; private set; }

    private GameObject Drop = null;
    private float currentTime = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Drop = Resources.Load<GameObject>("Actor/Enemy/Drop/Drop");

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= Game.Instance.DropSpawnDelay)
        {
            Spawn();
            currentTime -= Game.Instance.DropSpawnDelay;
        }
    }

    private void Spawn()
    {
        if (!Drop) return;

        Vector2 spawnPosition = Vector2.zero;
        spawnPosition.x = Random.Range(-1.5f, 1.5f);
        spawnPosition.y = 1.2f;

        Instantiate(Drop, spawnPosition, Quaternion.identity);
    }
}