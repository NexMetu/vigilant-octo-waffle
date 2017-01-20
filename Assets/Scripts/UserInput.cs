using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {

	private Player player;

	void Start () {
		player = GameObject.FindObjectOfType<Player>();
	}

	void Update () {
		// Move Forward / Back
		if(Input.GetKey(KeyCode.W)) {
			if(player) player.Move(Directions.Forward);
		} else if(Input.GetKey(KeyCode.S)) {
			if(player) player.Move(Directions.Backward);
		}
		// Move Left / Right
		if(Input.GetKey(KeyCode.A)) {
			if(player) player.Move(Directions.Left);
		} else if(Input.GetKey(KeyCode.D)) {
			if(player) player.Move(Directions.Right);
		}
		// Rotate Body
		if(Input.GetKey(KeyCode.Q)) {
			if(player) player.RotateBody(Directions.Left);
		} else if(Input.GetKey(KeyCode.E)) {
			if(player) player.RotateBody(Directions.Right);
		}
		//Rotate Head
		if(player) player.RotateHead(Directions.Horizontal, Input.GetAxis("Mouse X"));
		if(player) player.RotateHead(Directions.Vertical, Input.GetAxis("Mouse Y") * 1.8f);
	}
}
