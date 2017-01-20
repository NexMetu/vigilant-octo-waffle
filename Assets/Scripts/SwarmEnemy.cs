using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEnemy : Enemy {

	public float rotateSpeed = 5.0f;

	private float baseSpeed = 5.0f;
	private bool updatedSpeed = false;

	protected override void Update () {
		base.Update ();
		int seconds = (int)Time.time;
		if(seconds % 3 == 0) {
			if(!updatedSpeed) {
				baseSpeed = (int)(Random.value * 100) % 5;
				baseSpeed += 1;
				updatedSpeed = true;
			}
		} else updatedSpeed = false;

		transform.RotateAround(transform.position, transform.up, Time.deltaTime * baseSpeed * rotateSpeed);
//		transform.RotateAround(transform.position, transform.right, Time.deltaTime * rotateSpeed);
//		transform.RotateAround(transform.position, transform.forward, Time.deltaTime * rotateSpeed);
	}

//	private Vector3 originalDestination = new Vector3(-9999, -9999, -9999), adjustedDestination;
//
//	protected  override Vector3 GetDestination() {
//		Vector3 destination = target.transform.position;
//		if(destination == originalDestination) return adjustedDestination;
//		originalDestination = destination;
//		adjustedDestination = destination;
//		if(Random.value < 0.5f) {
//			adjustedDestination.x = adjustedDestination.x + (Random.value * xVariance + xOffset);
//		} else {
//			adjustedDestination.x = adjustedDestination.x - (Random.value * xVariance + xOffset);
//		}
//		if(Random.value < 0.5f) {
//			adjustedDestination.y = adjustedDestination.y + (Random.value * yVariance + yOffset);
//		} else {
//			adjustedDestination.y = adjustedDestination.y - (Random.value * yVariance + yOffset);
//		}
//		if(Random.value < 0.5f) {
//			adjustedDestination.z = adjustedDestination.z + (Random.value * zVariance + zOffset);
//		} else {
//			adjustedDestination.z = adjustedDestination.z - (Random.value * zVariance + zOffset);
//		}
//		return adjustedDestination;
//	}
//
//	void OnCollisionEnter(Collision collision){
//		Player player = collision.gameObject.GetComponentInParent<Player>();
//		if(player) {
//			//TODO: spawn explosion
//			player.TakeDamage(weaponDamage);
//			Die();
//		}
//	}

}
