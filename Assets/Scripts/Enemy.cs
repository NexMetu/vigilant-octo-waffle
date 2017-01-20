using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 1;
	public float speed = 10.0f;

	private Player target;

	// Use this for initialization
	void Start () {
		target = Component.FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: add AI logic on moving towards player / attacking them here
		if(target) {
			Vector3 destination = target.transform.position;
			destination.y += 1.0f;
			transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
		}
	}

	public void Damage(int damage) {
		health -= damage;
		if(health <= 0) Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision){
		Player player = collision.gameObject.GetComponentInParent<Player>();
		if(player) {
			//TODO: damage player
			Destroy(gameObject);
		}
	}
}
