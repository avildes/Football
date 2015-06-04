using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    public GameObject touchAudioSource;

    public GameObject encore;

    public GameObject player;

    /// <summary>
    /// The Active enemies.
    /// </summary>
    public List<GameObject> enemies;
    public List<GameObject> notActiveEnemies;

	public GameObject[] enemyPrefabs;
	private int currentEnemySet;

    public GameObject enemyPrefab;

    public GameObject scoreObj;

    public GameObject timerObj;

    public GameObject finalScoreObj;
    public GameObject recordObj;

    public GameObject retry;

    public GameObject hud;

    public GameObject tutorial;
    //APAGAR
    public GameObject retryCount;

    public bool gameOver = false;

    private bool create = false;

    private List<float> intervalosDeIncrementoDeVelocidade;
    private List<float> incrementosDeVelocidade;

    private List<float> intervalosDeIncrementoProbabilidadeSpawn;
    private List<float> incrementosProbabilidadeSpawn;

    private float tempoTotal;

    private AudioSource source;

    private AudioClip touchFX;
    private AudioClip dieFX;

    private float distanciaColisaoAposInimigo;
    private float distanciaColisaoAntesInimigo;

    private float enemySpawnYPosition;
    private float enemyDestroyYPosition;

    private bool enableCollision;

    private float scoreIncrement;

    private float distanciaEntreSpawns;

    private List<float> intervalosDistanciaEntreSpawns;
    private List<float> distanciasEntreSpawns;

    private float enemyPrepareDistance;
    private float enemyLookDistance;

    private int bestScore;
    private bool beatenHiScore = false;

    private int scoreCheckpoint;
    private int scoreCheck;
    private float enemySpeed;
    private float verticalDirection;

    private float score = 0;

    private AudioClip encoreClip;
    private AudioClip checkpointClip;

    private AudioSource touchSource;

    private bool resetHiScore;

    

    private bool play;

    private GameState gameState;

    private int speedIndex;
    private int distanceIndex;

    private float mainSpeed;

    private enum GameState
    {
        TUTORIAL,
        GAME,
        GAMEOVER
    }

    void Start()
    {
		currentEnemySet = UnityEngine.Random.Range(0, enemyPrefabs.Length);

        CollisionEventManager.onGameOver += GameOver;

        StartGame();
    }

    void StartGame()
    {


        tempoTotal = 0;
        score = 0;
        scoreObj.GetComponent<Text>().text = (int)score + "";

        SendGameOverToBGController(false);
        player.SetActive(true);
        hud.SetActive(true);
        retry.SetActive(false);

        AnalyticsManager.Instance.LogScene("Game");

        source = GetComponent<AudioSource>();
        source.loop = false;

        touchSource = touchAudioSource.GetComponent<AudioSource>();
        touchSource.loop = false;

        bestScore = LoadBest();

        //bestObj.GetComponent<Text>().text = bestScore.ToString();

        UpdateVariables();

        scoreCheck = scoreCheckpoint;

        if (resetHiScore)
        {
            SaveBest(0);
        }

        StartCoroutine(Init());
        //APAGAR
        retryCount.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = (LoadRetryCount()).ToString();

        speedIndex = 0;
        distanceIndex = 0;

        //SetSpeed(8);
        //SetSpawnDistance(4);
        //

        AdBehaviour.Instance.HideBannerView();


    }

    IEnumerator Init()
    {
        while(gameState == GameState.TUTORIAL)
        {
            tutorial.SetActive(true);

            yield return new WaitForSeconds(.5f);
        }

        tutorial.SetActive(false);
    }

    void UpdateVariables()
    {
        enemySpeed = BalanceClass.Instance.enemySpeed;
        verticalDirection = BalanceClass.Instance.enemyVerticalDirection;

        intervalosDeIncrementoDeVelocidade = BalanceClass.Instance.intervalosDeIncrementoDeVelocidade;
        incrementosDeVelocidade = BalanceClass.Instance.incrementosDeVelocidade;

        intervalosDeIncrementoProbabilidadeSpawn = BalanceClass.Instance.intervalosDeIncrementoProbabilidadeSpawn;
        incrementosProbabilidadeSpawn = BalanceClass.Instance.incrementosProbabilidadeSpawn;

        touchFX = BalanceClass.Instance.touchFX;
        dieFX = BalanceClass.Instance.dieFX;
        encoreClip = BalanceClass.Instance.encoreClip;
        checkpointClip = BalanceClass.Instance.checkpointClip;

        distanciaColisaoAposInimigo = BalanceClass.Instance.distanciaColisaoAposInimigo;
        distanciaColisaoAntesInimigo = BalanceClass.Instance.distanciaColisaoAntesInimigo;

        enemySpawnYPosition = BalanceClass.Instance.enemySpawnYPosition;
        enemyDestroyYPosition = BalanceClass.Instance.enemyDestroyYPosition;

        enableCollision = BalanceClass.Instance.enableCollision;

        scoreIncrement = BalanceClass.Instance.scoreIncrement;

        distanciasEntreSpawns = BalanceClass.Instance.distanciasEntreSpawns;
        intervalosDistanciaEntreSpawns = BalanceClass.Instance.intervalosDistanciaEntreSpawns;

        enemyPrepareDistance = BalanceClass.Instance.enemyPrepareDistance;
        enemyLookDistance = BalanceClass.Instance.enemyLookDistance;

        scoreCheckpoint = BalanceClass.Instance.scoreCheckpoint;

        resetHiScore = BalanceClass.Instance.resetHiScore;
    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
            MoveEnemies();
        }
    }

    void UpdateBalance()
    {
        SetValue(SetSpeed, intervalosDeIncrementoDeVelocidade, incrementosDeVelocidade, ref speedIndex);
        //SetValueAtTime(SetSpeed, intervalosDeIncrementoDeVelocidade, incrementosDeVelocidade);
        SetValue(SetSpawnDistance, intervalosDistanciaEntreSpawns, distanciasEntreSpawns, ref distanceIndex);

        //yield return new WaitForSeconds(1);

        //StartCoroutine(UpdateBalance());
    }

    void HandleInput()
    {
        //delete
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameState == GameState.TUTORIAL)
            {
                tutorial.SetActive(false);
                gameState = GameState.GAME;
            }
        }

        if (gameState == GameState.GAME)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MovePlayer(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MovePlayer(1);
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameState == GameState.GAMEOVER)
            {
                AnalyticsManager.Instance.LogSceneTransition("Retry", "Game");
                gameState = GameState.TUTORIAL;
                StartGame();
            }
        }*/
        ///dd


        if ((Input.touchCount == 1) && (Input.touches[0].phase.Equals(TouchPhase.Began)))
        {
            if (gameState == GameState.TUTORIAL)
            {
                tutorial.SetActive(false);
                gameState = GameState.GAME;
            }
            else if (gameState == GameState.GAME)
            {
                var touch = Input.touches[0];
                if (touch.position.x < Screen.width / 2)
                {
                    MovePlayer(-1);
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    MovePlayer(1);
                }
            }   
        }
    }

    public void Retry()
    {
        if (gameState == GameState.GAMEOVER)
        {
            AnalyticsManager.Instance.LogSceneTransition("Retry", "Game");
            gameState = GameState.TUTORIAL;


			currentEnemySet = UnityEngine.Random.Range(0, enemyPrefabs.Length);

			notActiveEnemies.Clear();
	

			GameObject.FindGameObjectWithTag("BackgroundControl").GetComponent<RandomSprite>().Randomize();


            StartGame();
        }
    }
/*
    void OnGUI()
    {
        if (gameState == GameState.GAME)
        {
            if (enableCollision)
            {
                if (Collides())
                {
                    GameOver();
                }
            }
        }
    }
    */
    void Update()
    {
        HandleInput();

        if (gameState == GameState.GAME)
        {
            tempoTotal += Time.deltaTime;

            UpdateBalance();
            UpdateScore();

            InstantiateEnemies();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AnalyticsManager.Instance.LogSceneTransition("Retry", "Splash");

                Application.LoadLevel("start");
            }
        }
    }

    void UpdateScore()
    {
        score += (scoreIncrement * enemySpeed);

        scoreObj.GetComponent<Text>().text = (int)score + "";

        if (score > scoreCheck)
        {
            PlayEncoreSound(checkpointClip);

            scoreCheck += scoreCheckpoint;
        }
        /*
        if (score > bestScore)
        {
            if (!beatenHiScore)
            {
                if (bestScore != 0) PlayEncoreSound(encoreClip);
                beatenHiScore = true;
            }

            bestObj.GetComponent<Text>().text = ((int)score).ToString();
        }
        */
    }

    bool CanCreate()
    {
        float expectedPosition = enemySpawnYPosition - distanciaEntreSpawns;

        if (enemies.Count < 1) return true;
        if (enemies.Count < 2 && enemies[0].transform.position.y < expectedPosition) return true;
        if (enemies[enemies.Count - 1].transform.position.y < expectedPosition) return true;
        return false;
    }

    void MovePlayer(int side)
    {
        PlayTouchFX();

        if (side == -1) // left
        {
            player.transform.position = new Vector3(-1, -4, 0);
        }
        else if (side == 1) // right
        {
            player.transform.position = new Vector3(1, -4, 0);
        }
    }


    bool Collides()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Vector3 enemyPos = enemies[i].transform.position;
            Vector3 playerPos = player.transform.position;

            float distance = enemyPos.y - playerPos.y;
            if (distance < distanciaColisaoAposInimigo) return false;

            if (((enemyPos.y - playerPos.y) < distanciaColisaoAntesInimigo) && (playerPos.x == enemyPos.x))
                return true;
        }
        return false;
    }

    void MoveEnemies()
    {
        Vector3 enemyPos;
        Vector3 playerPos = player.transform.position;

        for (int i = 0; i < enemies.Count; i++)
        {
            //enemies[i].transform.Translate(new Vector3(0, enemySpeed * verticalDirection * Time.deltaTime, 0));
            enemies[i].transform.Translate(new Vector3(0, mainSpeed, 0));

            if (enemies[i].transform.position.y < enemyDestroyYPosition)
            {
                //Destroy and instantiate can cause a big memory usage
                //Destroy(enemies[i]);
                enemies[i].SetActive(false);
                notActiveEnemies.Add(enemies[i]);
                enemies.RemoveAt(i);
                i--;
            }
            else
            {
                enemyPos = enemies[i].transform.position;

                float pos = playerPos.y + enemyPrepareDistance;

                if (enemyPos.y < pos)
                {
                    enemies[i].GetComponent<Animator>().SetTrigger("Prepare");
                }

                pos = playerPos.y + enemyLookDistance;
                /*
                if ((enemyPos.y < pos) && (playerPos.x != enemyPos.x))
                {
                    enemies[i].GetComponent<Animator>().SetInteger("Look", (int)playerPos.x);
                }
                */
                if (enemyPos.y < playerPos.y)
                {
                    enemies[i].GetComponent<SpriteRenderer>().sortingOrder = 5;
                }
            }
        }
    }

    void InstantiateEnemies()
    {
        if (CanCreate())
        {
            if (create)
            {
                float x = UnityEngine.Random.value;
                x = (x < .5f) ? -1 : 1;

                if ((enemies.Count + notActiveEnemies.Count) < 7)
                {
					GameObject enemyPrefab = enemyPrefabs[currentEnemySet];
					GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, enemySpawnYPosition, 0), Quaternion.identity) as GameObject;

//                    GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, enemySpawnYPosition, 0), Quaternion.identity) as GameObject;

                    enemies.Add(enemy);
                }
                else
                {
                    if (notActiveEnemies.Count <= 0) Debug.Log("NotActiveEnemies.Count <= 0");
                    GameObject enemy = notActiveEnemies[0];
                    notActiveEnemies.RemoveAt(0);

                    enemy.SetActive(true);
                    Vector3 enemyPosition = new Vector3(x, enemySpawnYPosition, 0);
                    enemy.transform.position = enemyPosition;

                    enemies.Add(enemy);
                }
            }

            SetValueAtTime(SetCreate, intervalosDeIncrementoProbabilidadeSpawn, incrementosProbabilidadeSpawn);
        }
    }

    void SetCreate(float chance)
    {
        float b = UnityEngine.Random.value;
        create = (b > chance) ? false : true;
    }

    void SetValue(Action<float> methodName, List<float> times, List<float> values, ref int index)
    {
        if (tempoTotal > times[index])
        {
            if (times.Count == index + 1) return;
            else index++;

            methodName(values[index]);

            return;
        }
    }

    void SetValueAtTime(Action<float> methodName, List<float> times, List<float> values)
    {
        for (int i = times.Count - 1; i > -1; i--)
        {
            if (tempoTotal > times[i])
            {
                methodName(values[i]);
                return;
            }
        }
    }

    void GameOver()
    {
        gameState = GameState.GAMEOVER;

        //play = false;
        //gameOver = true;

        //Fires the onGameOver event
        //onGameOver();
        //APAGAR
        //retryCount.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = (LoadRetryCount()).ToString();

        AnalyticsManager.Instance.LogScene("Retry");

        PlayDieFX();

        SendGameOverToBGController(true);
        player.SetActive(false);
        hud.SetActive(false);
        retry.SetActive(true);

        AnalyticsManager.Instance.LogTimeSpent("Match Duration", (int)tempoTotal);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SetActive(false);
            notActiveEnemies.Add(enemies[i]);
            enemies.RemoveAt(i);
            i--;
        }

        int sc = (int)score;
        int best;
        if (LoadBest() < sc)
        {
            AnalyticsManager.Instance.LogHiScore(sc);
            SaveBest(sc);
            best = sc;
        }
        else
        {
            best = LoadBest();
        }

        finalScoreObj.GetComponent<Text>().text = sc.ToString();

        recordObj.GetComponent<Text>().text = best.ToString();
    }

    void SendGameOverToBGController(bool value)
    {
        GameObject.FindGameObjectWithTag("BackgroundControl").SendMessage("SetGameOver", value, SendMessageOptions.DontRequireReceiver);
    }

    void PlayTouchFX()
    {
        touchSource.clip = touchFX;
        touchSource.Play();
    }

    void PlayDieFX()
    {
        source.clip = dieFX;
        source.Play();
    }

    void SaveBest(int _score)
    {
        PlayerPrefs.SetInt("Best", _score);
    }

    int LoadBest()
    {
        if (PlayerPrefs.HasKey("Best"))
        {
            return PlayerPrefs.GetInt("Best");

        }

        return 0;
    }
    // APAGAR
    int LoadRetryCount()
    {
        if (PlayerPrefs.HasKey("Retry"))
        {
            return PlayerPrefs.GetInt("Retry");
        }
        return 0;
    }

    void SetSpeed(float value)
    {
        float time = Time.deltaTime;
        time = time < 0.01f ? 0.015f : time;
        time = time > 0.03f ? 0.015f : time;

        mainSpeed = value * verticalDirection * time;
        
        //enemySpeed = value;
        //Debug.Log("Enemy speed set at: " + value);
        //GameObject.FindGameObjectWithTag("BackgroundControl").SendMessage("SetSpeed", value, SendMessageOptions.DontRequireReceiver);
        GameObject.FindGameObjectWithTag("BackgroundControl").SendMessage("SetSpeed", mainSpeed, SendMessageOptions.DontRequireReceiver);
    }

    void SetSpawnDistance(float value)
    {
        distanciaEntreSpawns = value;
    }

    void PlayEncoreSound(AudioClip clip)
    {
        AudioSource enc = encore.GetComponent<AudioSource>();
        enc.clip = clip;
        enc.loop = false;
        enc.Play();
    }

    void OnDestroy()
    {
        CollisionEventManager.onGameOver -= GameOver;
    }
}