using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	protected Player target;

	// Use this for initialization
	void Start () {
		target = Component.FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: add AI logic on moving towards player / attacking them here
		if(target) {
			Vector3 destination = GetDestination();
			destination.y += 1.0f;
			transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
			//			if(TargetInRange() && weapon) weapon.Fire(); 
		}
	}

	protected virtual Vector3 GetDestination() {
		return transform.position;
	}

	private bool TargetInRange() {
		if(!target) return false;
		return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
	}

	public void Damage(int damage) {
		health -= damage;
		if(health <= 0) Destroy(gameObject);
	}
}
