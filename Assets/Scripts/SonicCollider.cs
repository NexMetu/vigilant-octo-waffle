using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicCollider : MonoBehaviour {

	private bool detectCollisions = false;
	private bool playerCollisionStarted = false, enemyCollisionStarted = false;
	private SonicWeapon weapon;

	void Start () {
		weapon = GetComponentInParent<SonicWeapon>();
	}

	void OnTriggerEnter(Collider col) {
		if(!detectCollisions || playerCollisionStarted || enemyCollisionStarted) return;
		Player player = col.gameObject.GetComponentInParent<Player>();
		if(player) {
			playerCollisionStarted = true;
			weapon.HitPlayer(player);
		}
		Enemy enemy = col.gameObject.GetComponentInParent<Enemy>();
		if(enemy) {
			enemyCollisionStarted = true;
			weapon.HitEnemy(enemy);
		}
	}

	public void StartDetecting() {
		detectCollisions = true;
		playerCollisionStarted = false;
		enemyCollisionStarted = false;
	}

	public void StopDetecting() {
		detectCollisions = false;
	}
}
