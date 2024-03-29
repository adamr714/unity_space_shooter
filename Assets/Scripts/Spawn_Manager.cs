﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
	[SerializeField]
	private GameObject _enemyPrefab;
	
	[SerializeField]
	private GameObject _tripleShotPowerUpPrefab;
	
	[SerializeField]
	private GameObject _enemyContainer;
	
	[SerializeField]
	private GameObject[] powerups;
	
	
	private bool _stopSpawning = false;
	
    // Start is called before the first frame update
    void Start()
    {

    }


	public void StartSpawning()
	{
		StartCoroutine(SpawnEnemyRoutine());
		StartCoroutine(SpawnPowerUpRoutine());
	}

	IEnumerator SpawnEnemyRoutine()
	{
		yield return new WaitForSeconds(2.0f);

		while(_stopSpawning == false)
		{
			Vector3 spawnToPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
			GameObject newEnemy = Instantiate(_enemyPrefab, spawnToPos, Quaternion.identity);
			newEnemy.transform.parent = _enemyContainer.transform;
			yield return new WaitForSeconds(3);
		}
	}
	
	IEnumerator SpawnPowerUpRoutine()
	{
		yield return new WaitForSeconds(2.0f);
		
		while(_stopSpawning == false)
		{
			Vector3 spawnToPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
			int randomPowerUp = Random.Range(0,3);
			Instantiate(powerups[randomPowerUp], spawnToPos, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
		}
	}
	
	public void OnPlayerDeath()
	{
		_stopSpawning = true;
	}
}
