using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{

	[System.Serializable]
	public class EnemyUnit
	{
		public GameObject enemy;
		public int quantity;
	}

	[System.Serializable]
	public class Wave
	{
		public List<EnemyUnit> enemyUnits;
		public float timeBetweenSpawnsMin;
		public float timeBetweenSpawnsMax;
		public float timeBeforeEnemySpawn;
	}

	public Wave[] waves;
	public Transform spawnPoint;

	public TextMeshProUGUI waveText;        // Text để hiển thị sóng
	public TextMeshProUGUI remainEnemyText;   // Text mới để hiển thị số lượng quái vật còn lại
	private int currentWave = 0;

	public Base Tower;
	public GameObject victoryPanel;
	public GameObject defeatPanel;

	private bool isEnd = false;
	private void Start()
	{
		//AudioManeger.Instance.PlayMusic("Theme");
		Tower = FindObjectOfType<Base>();

		StartCoroutine(SpawnWaves());
		defeatPanel.SetActive(false);
		victoryPanel.SetActive(false);
	}

	private IEnumerator SpawnWaves()
	{
		Debug.Log("kkkk");
		for (; currentWave < waves.Length; currentWave++)
		{
			yield return new WaitForSeconds(waves[currentWave].timeBeforeEnemySpawn);
			UpdateWaveInfo(currentWave + 1);
			yield return StartCoroutine(SpawnWave(waves[currentWave]));

		}
	}

	private void Update()
	{
		UpdateRemainingEnemies();
		if (!isEnd) {
			if (currentWave >= waves.Length && CountActiveEnemies() == 0)
			{
			/*	AudioManeger.Instance.PlaySFX("win");*/
				victoryPanel.SetActive(true);
				string activeScene = SceneManager.GetActiveScene().name;
				string levelIndex = activeScene.Split(".")[0];
				PlayerPrefs.SetInt("Lv" + levelIndex, 1);
				Time.timeScale = 0;
				isEnd = true;
			}
			//lose
			if (Tower.currentHealth <= 0)
			{


				Debug.Log("destroy");
				defeatPanel.SetActive(true);
				/*AudioManeger.Instance.PlaySFX("lose");*/
				Time.timeScale = 0;
				isEnd = true;
			}
		} 
		//win 
		
	}


	private IEnumerator SpawnWave(Wave wave)
	{

		int indexEnemy = 0;
		float timeRandom = 0;
		
		while (wave.enemyUnits.Sum(x => x.quantity) > 0 && !isEnd)
		{
			indexEnemy = Random.Range(0, wave.enemyUnits.Count());
			timeRandom = Random.Range(wave.timeBetweenSpawnsMin, wave.timeBetweenSpawnsMax);
			Instantiate(wave.enemyUnits[indexEnemy].enemy, spawnPoint.position, spawnPoint.rotation);
            AudioManager.Instance.PlaySFX("enemySpawn");
            wave.enemyUnits[indexEnemy].quantity -= 1;
			if (wave.enemyUnits[indexEnemy].quantity == 0)
				wave.enemyUnits.Remove(wave.enemyUnits[indexEnemy]);
			yield return new WaitForSeconds(timeRandom);
		}


	}

	private void UpdateWaveInfo(int waveNumber)
	{
		waveText.text = "Wave: " + waveNumber;
	}

	public void UpdateRemainingEnemies()
	{
		remainEnemyText.text = "Quái vật: " + CountActiveEnemies();
	}

	public int CountActiveEnemies()
	{
		return GameObject.FindGameObjectsWithTag("Enemy").Length;
	}
}
