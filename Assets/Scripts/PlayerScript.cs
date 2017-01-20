using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript: NetworkBehaviour
{
	public GameObject bulletPrefab;
	public Transform[] bulletSpawn;
	public float coolDownPeriodInSeconds;
	public float timeStamp;

	void Stat(){
		timeStamp = Time.time;
	}

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);


		if(Input.GetMouseButton(0) && timeStamp <= Time.time)
		{
			Fire();
			timeStamp = Time.time + coolDownPeriodInSeconds;
		}
	}


	void Fire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn[0].position,
			bulletSpawn[0].rotation);

		var bullet2 = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn[1].position,
			bulletSpawn[1].rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;
		bullet2.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;
		// Destroy the bullet after 2 seconds
		Destroy(bullet, 20.0f);        
	}

	public override void OnStartLocalPlayer ()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
}