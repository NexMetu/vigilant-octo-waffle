using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public int bulletspeed;
	public int damage;

	public bool knockBack;

	void OnCollisionEnter(Collision collision){
		Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
		if(enemy) {
			enemy.Damage(damage);
			Destroy(gameObject);
		}
	}
}