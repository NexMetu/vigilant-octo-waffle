using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicShoot : MonoBehaviour {

	public float shotSpeed;


	// Use this for initialization
	void Start () {

		this.transform.localScale = new Vector3 (0, 0.3f, 1);


	}

	// Update is called once per frame
	void Update () {


		// Chords ////////////////////////
		if (Input.GetKey (KeyCode.Y)) {
			Debug.Log ("Chord I");
			ChordPressed (0);
		} else if (Input.GetKeyUp (KeyCode.Y)) {
			this.transform.localScale = new Vector3 (0, 0.3f, 1);
		}




	}

	private void ChordPressed(int chord) {

		switch(chord)
		{
		case 0:
			this.transform.localScale += new Vector3 (shotSpeed, 0, 0);
			break;
		case 1:
			//Chord_ii.transform.localScale += new Vector3 (shotSpeed, 0, 0);
			break;
		case 2:
			//Chord_iii.transform.localScale += new Vector3 (shotSpeed, 0, 0);
			break;
		default:
			break;
		}
	}
}
