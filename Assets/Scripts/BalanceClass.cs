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

	public List<float> intervalosDistanciaEntreSpawns = new List<float>();
	public List<float> distanciasEntreSpawns = new List<float>();
	
	public AudioClip touchFX;
    public AudioClip dieFX;

	public AudioClip encoreClip;
	public AudioClip checkpointClip;

    public float distanciaColisaoAposInimigo = 0f;
    public float distanciaColisaoAntesInimigo = 1f;

	public float scoreIncrement = .1f;

	public float enemyPrepareDistance = 2f;
	public float enemyLookDistance = 1f;

	public int scoreCheckpoint = 50;

	public bool resetHiScore;

    public static BalanceClass Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

}
