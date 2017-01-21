using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public int health = 1;
	public float speed = 10.0f;
	public float weaponRange = 3.0f;
	public int weaponDamage = 1;

	public float xOffset = 2.0f;
	public float yOffset = 2.0f;
	public float zOffset = 2.0f;

	public float xVariance = 3.0f;
	public float yVariance = 3.0f;
	public float zVariance = 3.0f;

	public AudioClip loop, death;

	protected Player target;
	protected AudioSource audioSource;

	private EnemySpawnPoint spawnPoint;
	private bool dying = false;
	private Text hpCounter;

	protected virtual void Start () {
		target = Component.FindObjectOfType<Player>();
		audioSource = GetComponent<AudioSource>();
		if(audioSource) {
			audioSource.clip = loop;
			audioSource.loop = true;
			audioSource.Play();
		}
		hpCounter = GetComponentInChildren<Text>();
	}

	protected virtual void Update () {
		if(hpCounter) hpCounter.text = health + "";
		if(dying) return;
		if(target) transform.position = Vector3.MoveTowards(transform.position, GetDestination(), Time.deltaTime * speed); 
		if(TargetInRange()) Attack();
	}

	protected virtual Vector3 GetDestination() {
		return transform.position;
	}

	protected virtual bool TargetInRange() {
		if(!target) return false;
		return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
	}

	protected virtual void Attack() {
		//nothing by default
	}

	protected void Die() {
		if(dying) return;
		dying = true;
		if(spawnPoint) spawnPoint.EnemyDestroyed();
		PreDestruction();
		if(PlayDeathRattle()) StartCoroutine(DeathSequence());
		else Destruction();
	}

	protected virtual void PreDestruction() {
		//do nothing
	}

	private IEnumerator DeathSequence() {
		if(audioSource) {
			audioSource.Stop();
			audioSource.clip = death;
			audioSource.loop = false;
			audioSource.Play();
		}
		yield return new WaitForSeconds(death.length);
		Destruction();
	}

	protected virtual void Destruction() {
		Destroy(gameObject);
	}

	protected virtual bool PlayDeathRattle() {
		return true;
	}

	public void SetSpawnPoint(EnemySpawnPoint spawnPoint) {
		this.spawnPoint = spawnPoint;
	}

	public void Damage(int damage) {
		health -= damage;
		if(health <= 0) Die();
	}
}
