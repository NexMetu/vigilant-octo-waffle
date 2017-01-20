using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

	public float speed = 1.0f;
	public float bodyRotateSpeed = 1.0f;
	public float headRotateSpeed = 1.0f;

	public float health = 100;
	public int armour = 10;
	public int ammo = 10;
	public int energy = 10;
	//Attributes for firing
	public float coolDownPeriodInSeconds;
	public float timeStamp;

	public Transform[] WeaponTypes;
	public int currentWeapon;

	private GameManager manager;
	private MechBody body;
	private MechHead head;

	// Use this for initialization
	void Start () {
		manager = Component.FindObjectOfType<GameManager>();
		body = GetComponentInChildren<MechBody>();
		head = GetComponentInChildren<MechHead>();

		currentWeapon = 0;
	}

	public void detectPressedKeyOrButton(){
		foreach(KeyCode k in Enum.GetValues(typeof(KeyCode))){
			if(Input.GetKeyDown(k)) Debug.Log("KeyCode down: " + k);
		}
	}
	// Update is called once per frame
	void Update () {
		detectPressedKeyOrButton ();
		if (Input.GetKeyDown ("r")) {
			currentWeapon = (currentWeapon + 1) % WeaponTypes.Length;
		} else if (Input.GetKeyDown ("t")) {
			currentWeapon = (currentWeapon + WeaponTypes.Length-1) % WeaponTypes.Length;
		}

		if(Input.GetMouseButton(0) && timeStamp <= Time.time)
		{	
			Debug.Log (WeaponTypes [currentWeapon].GetComponent<Weapons>());
			WeaponTypes[currentWeapon].GetComponent<Weapons>().Fire();
		}
		
	}

//	void Fire()
//	{
//		// Create the Bullet from the Bullet Prefab
//		var bullet = (GameObject)Instantiate(
//			bulletPrefab,
//			bulletSpawn.position,
//			bulletSpawn.rotation);
//		// Add velocity to the bullet
//		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bullet.GetComponent<Bullet>().bulletspeed;
//		// Destroy the bullet after 2 seconds
//		bullet.transform.Rotate(new Vector3(bullet.transform.rotation.x + 90, bullet.transform.rotation.y, bullet.transform.rotation.z));
//		Destroy(bullet, 20.0f);        
//	}

	public void Move(Directions direction) {
		Vector3 destination = transform.position;
		switch(direction) {
		case Directions.Forward:
			destination += body.transform.forward;
			break;
		case Directions.Backward:
			destination -= body.transform.forward;
			break;
		case Directions.Left:
			destination -= body.transform.right;
			break;
		case Directions.Right:
			destination += body.transform.right;
			break;
		default: break;
		}
		transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
	}

	public void RotateHead(Directions direction, float amount) {
		if(direction == Directions.Horizontal) {
			head.transform.RotateAround(transform.position, transform.up, Time.deltaTime * headRotateSpeed * amount);
		}
		if(direction == Directions.Vertical) {
			head.transform.RotateAround(head.transform.position, head.transform.right, Time.deltaTime * headRotateSpeed * amount);
			Vector3 headAngle = head.transform.rotation.eulerAngles;
			if(headAngle.x > 355.0f) {
				headAngle.x = 355.0f;
			} else if (headAngle.x < 270.0f && headAngle.x > 15.0f) {
				headAngle.x = 15.0f;
			}
			head.transform.eulerAngles = headAngle;
		}
	}

	public void RotateBody(Directions direction) {
		int directionModifier = 1;
		if(direction == Directions.Left) directionModifier = -1;
		transform.RotateAround(transform.position, transform.up, Time.deltaTime * bodyRotateSpeed * directionModifier);
	}

	void OnTriggerEnter(Collider col) {
		Powerup powerup = col.gameObject.GetComponent<Powerup>();
		if(powerup) {
			switch(powerup.type) {
			case PowerupType.Ammo:
				ammo += powerup.value;
				break;
			case PowerupType.Armour:
				armour += powerup.value;
				break;
			case PowerupType.Energy:
				energy += powerup.value;
				break;
			default: break;
			}
			Destroy(col.gameObject);
		}
	}

	public void TakeDamage(int damage) {
		health -= damage;
		//TODO: handle health<=0 -> dying / game over
	}
}
