using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 1.0f;
	public float bodyRotateSpeed = 1.0f;
	public float headRotateSpeed = 1.0f;

	//player ui elements
	public GameObject healthbar;
	public GameObject enegrybar;
	public GameObject sonicweapon;
	public GameObject laserweapon;

	public float maxHealth;
	public float health;
	public int armour = 10;
	public int ammo = 10;
	public float maxEnergy;
	public float energy = 10;
	//Attributes for firing
	public float coolDownPeriodInSeconds;
	public float timeStamp;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	private GameManager manager;
	private MechBody body;
	private MechHead head;
	private SonicWeapon weapon;

	void Start () {
		manager = Component.FindObjectOfType<GameManager>();
		body = GetComponentInChildren<MechBody>();
		head = GetComponentInChildren<MechHead>();
		weapon = GetComponentInChildren<SonicWeapon>();
		timeStamp = Time.time;
	}

	void Update () {
		setHealthBar ();
		setEnegrybar ();
		setWeaponImange ();
		
	}

	public void StartAttack() {
		if(weapon) weapon.StartFiring();
	}

	public void StopAttack() {
		if(weapon) weapon.StopFiring();
	}

	public void SwitchWeapon() {
		if(weapon) weapon.staticLength = !weapon.staticLength;
	}

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

//	void OnTriggerEnter(Collider col) {
//		Powerup powerup = col.gameObject.GetComponent<Powerup>();
//		if(powerup) {
//			Debug.Log("trigger powerup");
//			switch(powerup.type) {
//			case PowerupType.Ammo:
//				ammo += powerup.value;
//				break;
//			case PowerupType.Armour:
//				armour += powerup.value;
//				break;
//			case PowerupType.Energy:
//				energy += powerup.value;
//				break;
//			default: break;
//			}
//			Destroy(col.gameObject);
//		}
//	}

	void OnCollisionEnter(Collision collision) {
		Powerup powerup = collision.gameObject.GetComponent<Powerup>();
		if(powerup) {
			Debug.Log("collide powerup");
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
			Destroy(collision.gameObject);
		}
	}

	void setHealthBar(){
		var healthscale = health/maxHealth;
		if (healthscale < 0) {
			healthscale = 0;
		} else if (healthscale > 1) {
			healthscale = 1;
		}
		healthbar.transform.localScale = new Vector3 (healthscale, healthbar.transform.localScale.y, healthbar.transform.localScale.z);	
	}

	void setEnegrybar (){

		var energyscale = energy/maxEnergy;
		if (energyscale < 0) {
			energyscale = 0;
		} else if (energyscale > 1) {
			energyscale = 1;
		}
		enegrybar.transform.localScale = new Vector3 (energyscale, enegrybar.transform.localScale.y, enegrybar.transform.localScale.z);

	}

	void setWeaponImange(){
		if (weapon.staticLength) {
			laserweapon.gameObject.SetActive (true);
			sonicweapon.gameObject.SetActive (false);
		} else {
			laserweapon.gameObject.SetActive (false);
			sonicweapon.gameObject.SetActive (true);
		}

	}

	public void TakeDamage(int damage) {
		health -= damage;
		//TODO: handle health<=0 -> dying / game over
	}

	public bool IsDead() {
		return health <= 0;
	}
}

