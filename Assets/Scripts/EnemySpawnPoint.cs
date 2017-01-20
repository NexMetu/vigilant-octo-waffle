using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {

	public GameObject prefab;
	public float spawnDelay = 1.0f;
	public int maxToSpawn = 5;

	public float xOffset = 2.0f;
	public float yOffset = 2.0f;
	public float zOffset = 2.0f;

	public float xVariance = 3.0f;
	public float yVariance = 3.0f;
	public float zVariance = 3.0f;

	private float elapsedTime = 0.0f;
	private int numberSpawned = 0;

	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime > spawnDelay) {
			elapsedTime = 0.0f;
			SpawnEnemy();
		}
	}

	private void SpawnEnemy() {
		if(numberSpawned >= maxToSpawn) return;
		Vector3 spawnPosition = transform.position;
		if(Random.value < 0.5f) {
			spawnPosition.x = spawnPosition.x + (Random.value * xVariance + xOffset);
		} else {
			spawnPosition.x = spawnPosition.x - (Random.value * xVariance + xOffset);
		}
		if(Random.value < 0.5f) {
			spawnPosition.y = spawnPosition.y + (Random.value * yVariance + yOffset);
		} else {
			spawnPosition.y = spawnPosition.y - (Random.value * yVariance + yOffset);
		}
		if(Random.value < 0.5f) {
			spawnPosition.z = spawnPosition.z + (Random.value * zVariance + zOffset);
		} else {
			spawnPosition.z = spawnPosition.z - (Random.value * zVariance + zOffset);
		}
		GameObject newObject = Instantiate(prefab, spawnPosition, transform.rotation);
		Enemy newEnemy = newObject.GetComponent<Enemy>();
		if(newEnemy) newEnemy.SetSpawnPoint(this);
		numberSpawned++;
	}

	public void EnemyDestroyed() {
		numberSpawned--;
	}
}
