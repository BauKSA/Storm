using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private bool isChangingScene = false;

    public static Game Instance { get; private set; }

    public GameObject Player;
    public GameObject Seed;

    public float Health { get; set; } = 100;
    public int Lives { get; private set; } = 3;
    public int Points { get; private set; } = 0;
    public int Level { get; private set; } = 1;
    public int MaxSeeds { get; } = 5;
    public int Seeds { get; set; } = 0;
    public readonly float ThunderDamage = 100f;

    [Header("Thunder (output)")]
    public float ThunderSpawnDelay { get; private set; }
    public int AntCountLimit { get; private set; }
    public float AntDamage { get; private set; }

    [Header("Drops (output)")]
    public float DropSpawnDelay { get; private set; }
    public int DropCountLimit { get; private set; }

    public readonly float DropDamage = 10f;

    // ------------------------ NUEVO ------------------------
    [Header("Flower (output)")]
    public float FlowerDeathTime { get; private set; }
    // -------------------------------------------------------

    [Header("Leveling")]
    [SerializeField] private int BasePointsToLevel = 100;
    [SerializeField] private float XPGrowth = 1.15f;
    private int nextLevelXP = 100;

    [Header("Difficulty Curve")]
    public int MaxLevel = 15;

    public float ThunderStart = 7.5f;
    public float ThunderEnd = 2.0f;

    public int AntsStart = 2;
    public int AntsEnd = 6;

    public float DamageStart = 0.015f;
    public float DamageEnd = 0.25f;

    public float DropDelayStart = 1.5f;
    public float DropDelayEnd = 0.6f;

    public int DropsStart = 1;
    public int DropsEnd = 4;

    // ------------------------ NUEVO ------------------------
    [Header("Flower (curve)")]
    public float FlowerDeathStart = 5f; // nivel 1
    public float FlowerDeathEnd = 2f;   // nivel 15
    // -------------------------------------------------------

    private float k;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Parámetro de curva exponencial
        k = -Mathf.Log(0.05f) / (MaxLevel - 1);

        ApplyDifficulty();
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if(Health <= 0)
        {
            PlayerDeath();
            Lives--;
            Health = 100;
        }

        if (Lives < 0 && !isChangingScene)
        {
            isChangingScene = true;
            Lives = 0;
            StartCoroutine(GameOverDelay());
        }
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(Player);
        SceneManager.LoadScene("GameOver");
    }

    public void SumPoints(int p)
    {
        Points += p;

        if (Points >= nextLevelXP)
        {
            Level++;
            nextLevelXP += Mathf.RoundToInt(BasePointsToLevel * Mathf.Pow(XPGrowth, Level - 1));
            ApplyDifficulty();
        }
    }

    private void ApplyDifficulty()
    {
        float t = Mathf.Exp(-k * (Level - 1)); // 1 → 0 suavizado

        // -------- Thunder --------
        ThunderSpawnDelay = ThunderEnd + (ThunderStart - ThunderEnd) * t;
        AntDamage = DamageEnd + (DamageStart - DamageEnd) * t;
        Debug.Log("Ant Damage set to: " + AntDamage + " with level " + Level + " and t = " + t);

        float antsF = AntsEnd + (AntsStart - AntsEnd) * t;
        AntCountLimit = Mathf.RoundToInt(antsF);

        // -------- Drops --------
        DropSpawnDelay = DropDelayEnd + (DropDelayStart - DropDelayEnd) * t;

        float dropsF = DropsEnd + (DropsStart - DropsEnd) * t;
        DropCountLimit = Mathf.RoundToInt(dropsF);

        // -------- Flower --------
        FlowerDeathTime = FlowerDeathEnd + (FlowerDeathStart - FlowerDeathEnd) * t;
    }

    void PlayerDeath()
    {
        if (!Player) return;

        Seeds = 0;

        MovementController playerMovement = Player.GetComponent<MovementController>();
        if (playerMovement) playerMovement.Stop();

        GameObject[] ants = GameObject.FindGameObjectsWithTag("ant");

        for (int i = 0; i < ants.Length; i++){
            AntBeingAlive antBeingAlive = ants[i].GetComponent<AntBeingAlive>();
            antBeingAlive.closestTarget = antBeingAlive.GetClosestTarget(false);
        }

        AnimationController playerAnimation = Player.GetComponent<AnimationController>();
        PlayerState playerState = Player.GetComponent<PlayerState>();

        playerState.Death();
        playerAnimation.StartAnimation("Player_death", true, () => {
            SpriteRenderer playerSprite = Player.GetComponent<SpriteRenderer>();
            playerSprite.enabled = false;

            StartCoroutine(RespawnAfterDelay());
        });
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        SpriteRenderer playerSprite = Player.GetComponent<SpriteRenderer>();
        AnimationController playerAnimation = Player.GetComponent<AnimationController>();
        PlayerState playerState = Player.GetComponent<PlayerState>();

        playerState.Respawn();
        playerAnimation.StartAnimation("Player_idle");
        playerSprite.enabled = true;
    }
}