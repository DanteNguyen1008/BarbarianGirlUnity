using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject[] spawnPoints;

    [SerializeField]
    GameObject[] powerUpPoints;

    [SerializeField]
    GameObject tanker;

    [SerializeField]
    GameObject ranger;

    [SerializeField]
    GameObject soldier;

    [SerializeField]
    Text levelText;

    [SerializeField]
    GameObject arrow;

    [SerializeField]
    GameObject heathPowerUp;

    [SerializeField]
    int maxPowerUps = 4;

    private bool isGameOver = false;
    private int currentLevel;
    private float generatedSpawnTime = 1;
    private float currentSpawnTime = 0;
    private float powerUpSpawnTime = 5;
    private float currentPowerUpSpawnTime = 0;
    private int powerUpsCount = 0;

    private GameObject newEnemy;

    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad (gameObject);
    }

    // Use this for initialization
    void Start () { 
        StartCoroutine(this.Spawn());
        StartCoroutine(this.SpawnPowerUp());
        this.currentLevel = 1;
        this.levelText.text = "Level " + this.currentLevel;
	}
	
	// Update is called once per frame
	void Update () {
        this.currentSpawnTime += Time.deltaTime;
        this.currentPowerUpSpawnTime += Time.deltaTime;
	}

    IEnumerator Spawn()
    {
        if (this.currentSpawnTime > this.generatedSpawnTime)
        {
            this.currentSpawnTime = 0;
            if(this.enemies.Count < this.currentLevel * 2)
            {
                var spawnLocation = this.spawnPoints[Random.Range(0, spawnPoints.Length - 1)];

                int randomEnemy = Random.Range(0, 3);
                
                if(randomEnemy == 0)
                {
                    this.newEnemy = Instantiate(soldier) as GameObject;
                }
                else if (randomEnemy == 1)
                {
                    this.newEnemy = Instantiate(tanker) as GameObject;
                }
                else if (randomEnemy == 2)
                {
                    this.newEnemy = Instantiate(ranger) as GameObject;
                }

                newEnemy.transform.position = spawnLocation.transform.position;
            }

            if (this.killedEnemies.Count == this.currentLevel * 2)
            {
                this.enemies.Clear();
                this.killedEnemies.Clear();

                yield return new WaitForSeconds(3f);

                this.currentLevel++;
                this.levelText.text = "Level " + this.currentLevel;
            }
        }

        yield return null;
        StartCoroutine(this.Spawn());
    }

    IEnumerator SpawnPowerUp()
    {
        if (this.currentPowerUpSpawnTime > this.powerUpSpawnTime)
        {
            this.currentPowerUpSpawnTime = 0;
            if (this.powerUpsCount <= this.maxPowerUps)
            {
                var powerUpSpawnLocation = this.powerUpPoints[Random.Range(0, powerUpPoints.Length - 1)];
                var _powerUp = Instantiate(heathPowerUp);
                _powerUp.transform.position = powerUpSpawnLocation.transform.position;
            }
        }

        yield return null;
        StartCoroutine(this.SpawnPowerUp());
    }

    public void PlayerHit(int currentHP)
    {
        if(currentHP > 0)
        {
            this.isGameOver = false;
        }
        else
        {
            this.isGameOver = true;
        }
    }

    public void RegisterEnemy(EnemyHealth enemy)
    {
        this.enemies.Add(enemy);
    }

    public void KilledEnemy(EnemyHealth enemy)
    {
        this.killedEnemies.Add(enemy);
    }

    public void RegisterPowerUp()
    {
        this.powerUpsCount++;
    }

    public void UnRegisterPowerUp()
    {
        this.powerUpsCount--;
    }

    public bool IsGameOver
    {
        get { return this.isGameOver; }
    }

    public GameObject Player
    {
        get { return this.player; }
    }

    public GameObject Arrow
    {
        get { return this.arrow; }
    }
}
