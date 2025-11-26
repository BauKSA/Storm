using UnityEngine;
using UnityEngine.UIElements;

public class ThunderSpawner : MonoBehaviour
{
    private float currentTime = 0f;

    public GameObject thunder = null;
    public GameObject flash = null;
    public GameObject mud = null;
    public GameObject ant = null;

    private Vector2 xRange = new(-1.25f, 1.25f);
    private Vector2 yRange = new(-0.5f, 0.5f);

    private float x = 0f;
    private float y = 0f;
    private float rotation = 0f;

    bool flashActive = false;
    readonly float flashDuration = 2f;

    private void Spawn()
    {
        flashActive = true;

        // Posición aleatoria
        x = Random.Range(xRange.x, xRange.y);
        y = Random.Range(yRange.x, yRange.y);
        Vector3 spawnPos = new(x, y, 0);

        // Rotación aleatoria (solo en Z para 2D)
        rotation = Random.Range(0f, 360f);
        Quaternion rot = Quaternion.Euler(0, 0, 0);

        // Instanciar
        if (!flash) return;
        GameObject flashObject = Instantiate(flash, spawnPos, rot);
        flashObject.GetComponent<ThunderFlashBeignAlive>().SetFlashDuration(flashDuration);

        // Escala fija en 1,1
        flashObject.transform.localScale = Vector3.one;
    }

    private void SpawnThunder()
    {
        flashActive = false;

        Vector3 spawnPos = new(x, y, 0);
        Quaternion rot = Quaternion.Euler(0, 0, rotation);

        if (!mud) return;

        GameObject mudObject = Instantiate(mud, new Vector3(spawnPos.x, spawnPos.y + 0.1f), Quaternion.identity);
        mudObject.transform.localScale = Vector3.one;
        MudBeignAlive mudAlive = mudObject.GetComponent<MudBeignAlive>();
        mudAlive.Ant = ant;

        if (!thunder) return;
        GameObject thunderObject = Instantiate(thunder, spawnPos, rot);
        thunderObject.transform.localScale = Vector3.one;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        
        if (flashActive)
        {
            if (currentTime >= flashDuration)
            {
                currentTime -= flashDuration;
                SpawnThunder();
            }
        }
        else
        {
            if (currentTime >= Game.Instance.ThunderSpawnDelay)
            {
                Debug.Log($"Spawn at time: {currentTime}");
                currentTime -= Game.Instance.ThunderSpawnDelay;
                Spawn();
            }
        }
    }
}