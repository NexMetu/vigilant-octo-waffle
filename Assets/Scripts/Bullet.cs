using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public bool knockBack;

	void OnTriggerEnter(Collision collision){
		collision.gameObject.GetComponent<Rigidbody> ().velocity = new Vector2 (10000000, 100000000);
		knockBack = true;
	}

	void OnCollisionEnter(Collision collision){

		var hit = collision.gameObject;
		var health = hit.GetComponent<Health> ();
		if (health != null){
			health.TakeDamge (10);
		}
		if (health != null) {
			Destroy (gameObject);
		}
	}	
}