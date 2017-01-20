using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEnemy : Enemy {

	private Vector3 originalDestination, adjustedDestination;

	protected  override Vector3 GetDestination() {
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

	void OnCollisionEnter(Collision collision){
		Player player = collision.gameObject.GetComponentInParent<Player>();
		if(player) {
			//TODO: spawn explosion
			player.TakeDamage(weaponDamage);
			Destroy(gameObject);
		}
	}

}
