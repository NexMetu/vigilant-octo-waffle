using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEnemy : Enemy {

	public float rotateSpeed = 5.0f;

	private float baseSpeed = 5.0f;
	private bool updatedSpeed = false, isRotating = true, madeAttack = false;
	private Vector3 actualDestination;

	private SwarmItem[] items;

	protected override void Start() {
		items = GetComponentsInChildren<SwarmItem>();
		base.Start();
	}

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
	}

	protected override Vector3 GetDestination () {
		actualDestination = target.transform.position;
		actualDestination.y = transform.position.y;
		return actualDestination;
	}

	protected override bool TargetInRange () {
		if(target && transform.position == actualDestination) return true;
		return false;
	}

	protected override void Attack () {
		if(madeAttack) return;
		base.Attack ();
		foreach(SwarmItem item in items) {
			item.InitiateAttack(Random.value * 0.5f);
		}
		madeAttack = true;
	}

}
