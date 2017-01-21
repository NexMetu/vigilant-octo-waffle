using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmItem : Enemy {

	private Vector3 originalDestination = new Vector3(-9999, -9999, -9999), adjustedDestination;

	private bool attacking = false;

	private float attackStartTime = -1.0f;

	protected override void Update () {
		if(attacking && Time.time > attackStartTime) {
			AudioSource audioSource = GetComponent<AudioSource>();
			if(audioSource && !audioSource.isPlaying) audioSource.Play();
			base.Update();
		}
	}

	protected override Vector3 GetDestination() {
		Vector3 destination = target.transform.position;
		if(destination == originalDestination) return adjustedDestination;
		originalDestination = destination;
		adjustedDestination = destination;
		if(Random.value < 0.5f) {
			adjustedDestination.x = adjustedDestination.x + (Random.value * xVariance + xOffset);
		} else {
			adjustedDestination.x = adjustedDestination.x - (Random.value * xVariance + xOffset);
		}
		if(Random.value < 0.5f) {
			adjustedDestination.y = adjustedDestination.y + (Random.value * yVariance + yOffset);
		} else {
			adjustedDestination.y = adjustedDestination.y - (Random.value * yVariance + yOffset);
		}
		if(Random.value < 0.5f) {
			adjustedDestination.z = adjustedDestination.z + (Random.value * zVariance + zOffset);
		} else {
			adjustedDestination.z = adjustedDestination.z - (Random.value * zVariance + zOffset);
		}
		return adjustedDestination;
	}

	public void InitiateAttack(float attackDelay) {
		attacking = true;
		attackStartTime = Time.time + attackDelay;
	}

	void OnCollisionEnter(Collision collision){
		Player player = collision.gameObject.GetComponentInParent<Player>();
		if(player) {
			//TODO: spawn explosion
			player.TakeDamage(weaponDamage);
			Die();
		}
	}
}
