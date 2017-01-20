using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public int bulletspeed;

	public bool knockBack;

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