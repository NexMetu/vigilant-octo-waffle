using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : Enemy {

	public float weaponCooldown = 5.0f;

	private Vector3 actualDestination;
	private SonicWeapon weapon;
	private float nextFireTime;

	protected override void Start() {
		weapon = GetComponentInChildren<SonicWeapon>();
		base.Start();
	}

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

	protected override void Attack () {
		base.Attack ();
		if(weapon && Time.time > nextFireTime) {
			weapon.StartFiring();
			nextFireTime = Time.time + weaponCooldown;
		}
	}

	protected override void Destruction () {
		//don't destroy, drop from the sky instead
		Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
		if(rigidbody) {
			rigidbody.useGravity = true;
			rigidbody.isKinematic = false;
		}
//		StartCoroutine(ResetRigidbody());
	}

	private IEnumerator ResetRigidbody() {
		yield return new WaitForSeconds(3.0f);
		Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
		if(rigidbody) {
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;
		}
	}

}
