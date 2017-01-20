using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {
	//weapons bullet model, and spawner
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	//cooldown of firing
	public float coolDownPeriodInSeconds;
	public float timeStamp;
	public float range;
	public bool addRotation;
	// Use this for initialization
	void Start () {
		timeStamp = Time.time;
	}

	public void Fire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletPrefab.transform.rotation);
		// Add velocity to the bullet
		//bullet.GetComponent<Rigidbody>().rigidbody.AddForce(Vector3.forward * Time.deltaTime * speed);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bullet.GetComponent<Bullet>().bulletspeed;
		if(addRotation)	bullet.transform.Rotate(new Vector3(bullet.transform.rotation.x + 90, bullet.transform.rotation.y, bullet.transform.rotation.z));
		// Destroy the bullet after 2 seconds
		Destroy(bullet, range);        
	}
	// Update is called once per frame
	void Update () {
		
	}
}
