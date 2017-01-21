using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicShoot : MonoBehaviour {

	public float maxSize = 2.0f;
	public float shotSpeed;

	private int maxIterations = 40;
	private int iteration = 0;

	private bool animating = false, extending = true;

	void Start () {
		this.transform.localScale = new Vector3 (0, 0.3f, 1);
	}

	void Update () {
		if(animating) {
			if(extending) {
				transform.localScale += new Vector3 (shotSpeed / 100.0f, 0, 0);
				if(transform.localScale.x >= maxSize) {
					Vector3 temp = transform.localScale;
					temp.x = maxSize;
					transform.localScale = temp;
					extending = false;
				}
			} else {
				transform.localScale -= new Vector3 (shotSpeed / 100.0f, 0, 0);
				if(transform.localScale.x <= 0.0f) {
					Vector3 temp = transform.localScale;
					temp.x = 0.0f;
					transform.localScale = temp;
					animating = false;
					extending = true;
				}
			}
		}


//		if(animating) {
//			if(extending) {
//				if(iteration == maxIterations) {
//					extending = false;
//				} else {
//					transform.localScale += new Vector3 (shotSpeed, 0, 0);
//					iteration++;
//				}
//			} else {
//				if(iteration == 0) {
//					animating = false;
//					extending = true;
//				} else {
//					transform.localScale -= new Vector3 (shotSpeed, 0, 0);
//					iteration--;
//				}
//			}
		 else {
			if(Input.GetKeyDown(KeyCode.Y)) {
				animating = true;
			}
		}
	}

	private void ChordPressed(int chord) {
		switch(chord)
		{
		case 0:
			this.transform.localScale += new Vector3 (shotSpeed, 0, 0);
			break;
		default:
			break;
		}
	}
}
