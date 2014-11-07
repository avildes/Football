using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalanceClass : MonoBehaviour
{
	public bool enableCollision = true;

    public float enemySpawnYPosition = 2f;
    public float enemyDestroyYPosition = -5f;

    public float enemySpeed = 2f;
    public float enemyVerticalDirection = -1f;

    public float bgSpeed = 2f;
    public float bgVerticalDirection = -1f;

    public List<float> intervalosDeIncrementoDeVelocidade = new List<float>();
    public List<float> incrementosDeVelocidade = new List<float>();

    public List<float> intervalosDeIncrementoProbabilidadeSpawn = new List<float>();
    public List<float> incrementosProbabilidadeSpawn = new List<float>();

    public AudioClip touchFX;

    public AudioClip dieFX;

    public float distanciaColisaoAposInimigo = 0f;
    public float distanciaColisaoAntesInimigo = 1f;

	public float scoreIncrement = .1f;

	public int distanciaEntreSpawns = 1;

    public static BalanceClass Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

}
