using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private float enemySpeed;
    private float verticalDirection;

    private float score = 0;

    public GameObject player;

    public List<GameObject> enemies;

    public GameObject enemyPrefab;

    public GameObject scoreObj;

    public GameObject timerObj;

    public GameObject bestObj;

    public GameObject retry;

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

	private int distanciaEntreSpawns;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = false;

        bestObj.GetComponent<TextMesh>().text = LoadBest().ToString();

        UpdateVariables();
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

        distanciaColisaoAposInimigo = BalanceClass.Instance.distanciaColisaoAposInimigo;
        distanciaColisaoAntesInimigo = BalanceClass.Instance.distanciaColisaoAntesInimigo;

        enemySpawnYPosition = BalanceClass.Instance.enemySpawnYPosition;
        enemyDestroyYPosition = BalanceClass.Instance.enemyDestroyYPosition;

		enableCollision = BalanceClass.Instance.enableCollision;

		scoreIncrement = BalanceClass.Instance.scoreIncrement;

		distanciaEntreSpawns = BalanceClass.Instance.distanciaEntreSpawns;
    }

    void GetTouches()
    {
        if ((Input.touchCount == 1) && (Input.touches[0].phase.Equals(TouchPhase.Ended)))
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

    void GetKeys()
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

    void Update()
    {
        if (!gameOver)
        {
			tempoTotal += Time.deltaTime;
			UpdateVariables();
			SetSpeedAtTime(intervalosDeIncrementoDeVelocidade, incrementosDeVelocidade);
            
			UpdateScore();
			/*
            increment += Time.deltaTime;
            if (score > 100) increment += Time.deltaTime;
            
            Vector3 size = timerObj.transform.localScale;
            size.x -= (Time.deltaTime * increment);

            timerObj.transform.localScale = size;

            if (timerObj.transform.localScale.x <= 0)
            {
                GameOver();
            }
            */
            GetTouches();
            GetKeys();

            MoveEnemies();
            InstantiateEnemies();

			if(enableCollision)
			{
	            if (Collides())
	            {
	               GameOver();
	            }
			}
        }
        else if ((Input.touchCount == 1 && Input.touches[0].phase.Equals(TouchPhase.Ended)) || Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("game");
        }
		else
		{
			if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel("start"); }
		}
    }

	void UpdateScore()
	{
		score += (scoreIncrement * enemySpeed);
		scoreObj.GetComponent<TextMesh>().text = (int)score + "";
	}

    bool CanCreate()
    {
        if (enemies.Count < 1) return true;
        if (enemies.Count < 2 && enemies[0].transform.position.y < distanciaEntreSpawns) return true;
		if (enemies[enemies.Count - 1].transform.position.y < distanciaEntreSpawns) return true;
        return false;
    }

    void MovePlayer(int side)
    {
        PlayTouchFX();

        /*
        Vector3 size = timerObj.transform.localScale;
        size.x += (1 / increment) * 400;
        if (size.x < 25) size.x = 25;
        timerObj.transform.localScale = size;

        if (timerObj.transform.localScale.x > 200)
        {
            size = timerObj.transform.localScale;
            size.x = 200;
            timerObj.transform.localScale = size;
        }
        */
        
        if (side == -1) // left
        {
            player.transform.position = new Vector3(-1, -4, 0);
        }
        else if (side == 1) // right
        {
            player.transform.position = new Vector3(1, -4, 0);
        }
        /*
        MoveEnemy();

        if (Collides())
        {
            GameOver();
        }*/
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
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.Translate(new Vector3(0, enemySpeed * verticalDirection * Time.deltaTime, 0));

            if (enemies[i].transform.position.y < enemyDestroyYPosition)
            {
                Destroy(enemies[i]);
                enemies.RemoveAt(i);
                i--;
            }
            else
            {
                Vector3 pos = enemies[i].transform.position;
                //pos.y -= 2;
                if (pos.y < -4)
                {
                    enemies[i].GetComponent<SpriteRenderer>().sortingOrder = 5;
                }
                //enemies[i].transform.position = pos;
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

                GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, enemySpawnYPosition, 0), Quaternion.identity) as GameObject;

                enemies.Add(enemy);
            }

            SetSpawnChanceAtTime(intervalosDeIncrementoProbabilidadeSpawn, incrementosProbabilidadeSpawn);
            /*
            if (score < 20)
            {
                float b = UnityEngine.Random.value;
                create = (b < .5f) ? false : true;
            }
            else if (score < 40)
            {
                float b = UnityEngine.Random.value;
                create = (b < .3f) ? false : true;
            }
            else
            {
                float b = UnityEngine.Random.value;
                create = (b < .1f) ? false : true;
            }*/
        }
    }

    void SetCreate(float chance)
    {
        float b = UnityEngine.Random.value;
        create = (b > chance) ? false : true;
    }

    void SetSpawnChanceAtTime(List<float> times, List<float> values)
    {
        for (int i = times.Count - 1; i > -1; i--)
        {
            if (tempoTotal > times[i])
            {
                SetCreate(values[i]);
                return;
            }
        }
    }

    void SetSpeedAtTime(List<float> times, List<float> values)
    {
        for (int i = times.Count-1; i > -1; i--)
        {
            if(tempoTotal > times[i])
            {
                SetSpeed(values[i]);
                return;
            }
        }
    }

    void GameOver()
    {
        SendGameOverToBGController(true);

        PlayDieFX();

        player.SetActive(false);
        retry.SetActive(true);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SetActive(false);
        }

        gameOver = true;

		int sc = (int) score;

        if (LoadBest() < sc)
        {
            SaveBest(sc);
            bestObj.GetComponent<TextMesh>().text = (sc).ToString();
        }
    }

    void SendGameOverToBGController(bool value)
    {
        GameObject.FindGameObjectWithTag("BackgroundControl").SendMessage("SetGameOver", value, SendMessageOptions.DontRequireReceiver);
    }

    void PlayTouchFX()
    {
        source.clip = touchFX;
        source.Play();
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

    void SetSpeed(float value)
    {
        enemySpeed = value;
        GameObject.FindGameObjectWithTag("BackgroundControl").SendMessage("SetSpeed", value, SendMessageOptions.DontRequireReceiver);
    }
}