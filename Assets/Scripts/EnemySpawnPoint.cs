using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {

	public GameObject enemyOne;
	public float spawnDelay;

	private float elapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime > spawnDelay) {
			elapsedTime = 0.0f;
			SpawnEnemy();
		}
	}

	private void SpawnEnemy() {
		Vector3 spawnPosition = transform.position;
		float offset = 2.0f;
		float range = 3.0f;
		if(Random.value < 0.5f) {
			spawnPosition.x = spawnPosition.x + (Random.value * range + offset);
		} else {
			spawnPosition.x = spawnPosition.x - (Random.value * range + offset);
		}
		if(Random.value < 0.5f) {
			spawnPosition.z = spawnPosition.z + (Random.value * range + offset);
		} else {
			spawnPosition.z = spawnPosition.z - (Random.value * range + offset);
		}
		spawnPosition.y = spawnPosition.y + (Random.value * range + offset);
		float shift = Random.value * range + offset;

		// Create the Bullet from the Bullet Prefab
		var enemy = (GameObject)Instantiate(
			enemyOne,
			spawnPosition,
			transform.rotation);
	}
}
