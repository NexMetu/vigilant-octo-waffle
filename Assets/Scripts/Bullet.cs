using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public int bulletspeed;
	public int damage;

	private bool hadCollision = false;

	public bool knockBack;

	void OnCollisionEnter(Collision collision){
		if(hadCollision) return;
		Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
		if(enemy) {
			hadCollision = true;
			enemy.Damage(damage);
			Destroy(gameObject);
		}
	}
}