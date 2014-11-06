using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private int score = 0;

    public GameObject player;

    public List<GameObject> enemies;

    public GameObject enemyPrefab;

    public GameObject scoreObj;

    public GameObject timerObj;

    public GameObject bestObj;

    public GameObject retry;

    public bool gameOver = false;

    private bool create = false;

    private float increment = 50;

    private AudioSource source;

    public AudioClip touchFX;
    public AudioClip dieFX;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = false;

        bestObj.GetComponent<TextMesh>().text = LoadBest().ToString();
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
            increment += Time.deltaTime;
            if (score > 100) increment += Time.deltaTime;

            Vector3 size = timerObj.transform.localScale;
            size.x -= (Time.deltaTime * increment);

            timerObj.transform.localScale = size;

            if (timerObj.transform.localScale.x <= 0)
            {
                GameOver();
            }

            GetTouches();
            GetKeys();
        }
        else if((Input.touchCount == 1 && Input.touches[0].phase.Equals(TouchPhase.Ended)) || Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("game");
        }
    }

    void MovePlayer(int side)
    {
        PlayTouchFX();

        score++;

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

        scoreObj.GetComponent<TextMesh>().text = score + "";

        if (side == -1) // left
        {
            player.transform.position = new Vector3(-1, -4, 0);
        }
        else if (side == 1) // right
        {
            player.transform.position = new Vector3(1, -4, 0);
        }

        MoveEnemy();

        if (Collides())
        {
            GameOver();
        }
    }

    bool Collides()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (player.transform.position.Equals(enemies[i].transform.position))
                return true;
        }
        return false;
    }

    void MoveEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].transform.position.y < -4)
            {
                Destroy(enemies[i]);
                enemies.RemoveAt(i);
                i--;
            }
            else
            {
                Vector3 pos = enemies[i].transform.position;
                pos.y -= 2;
                enemies[i].transform.position = pos;
            }
        }

        if (create)
        {
            float x = UnityEngine.Random.value;

            x = (x < .5f) ? -1 : 1;

            GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, 2, 0), Quaternion.identity) as GameObject;

            enemies.Add(enemy);
        }

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
        }
    }

    void GameOver()
    {
        PlayDieFX();

        player.SetActive(false);
        retry.SetActive(true);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SetActive(false);
        }

        gameOver = true;

        if (LoadBest() < score)
        {
            SaveBest(score);
            bestObj.GetComponent<TextMesh>().text = score.ToString();
        }
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

    void SaveBest(int score)
    {
        PlayerPrefs.SetInt("Best", score);
    }

    int LoadBest()
    {
        if (PlayerPrefs.HasKey("Best"))
        {
            return PlayerPrefs.GetInt("Best", score);
        }

        return 0;
    }
}