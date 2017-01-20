using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : Enemy {

	private Vector3 actualDestination;

	protected override Vector3 GetDestination() {
		Vector3 destination = target.transform.position;
		Vector3 direction = Vector3.Normalize(transform.position - target.transform.position);
		float distance = weaponRange - 1;
		actualDestination = destination + (distance * direction);
		actualDestination.y += 1.0f;
		return actualDestination;
	}

	protected override bool TargetInRange () {
		if(target && transform.position == actualDestination) return true;
		return false;
	}

}
