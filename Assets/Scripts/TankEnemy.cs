using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : Enemy {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected  override Vector3 GetDestination() {
		return target.transform.position;
	}
}
