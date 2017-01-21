using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWeapon : MonoBehaviour {

	public float maxSize = 2.0f;
	public float shotSpeed;
	public bool staticLength = false;
	public Color baseColor = Color.red;
	public AudioClip staticClip, expandingClip;

	private bool animating = false, extending = true;
	private AudioSource audioSource;

	void Start () {
		foreach(Transform child in transform) {
			child.localScale = new Vector3 (0.0f, 0.3f, 1.0f);
		}
		foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) {
			renderer.color = baseColor;
		}
		audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		if(animating) {
			if(extending) {
				foreach(Transform child in transform) {
					child.localScale += new Vector3 (shotSpeed / 100.0f, 0.0f, 0.0f);
					float limit = child.name.Contains("BroadWave") ? maxSize / 2.55f : maxSize;
					if(child.localScale.x >= limit) {
						child.localScale = new Vector3 (limit, 0.3f, 1.0f);
						if(!staticLength) extending = false;
					}
				}
			} else {
				foreach(Transform child in transform) {
					child.localScale -= new Vector3 (shotSpeed / 100.0f, 0.0f, 0.0f);
					if(child.localScale.x <= 0.0f) {
						animating = false;
						StopFiring();
					}
				}
			}
		}
	}

	public void StartFiring() {
		if(animating) return;
		if(audioSource) {
			audioSource.clip = staticLength ? staticClip : expandingClip;
			audioSource.Play();
		}
		animating = true;
		extending = true;
	}

	public void StopFiring() {
		if(!staticLength && animating) return;
		if(audioSource) audioSource.Stop();
		foreach(Transform child in transform) {
			child.localScale = new Vector3 (0.0f, 0.3f, 1.0f);
		}
		animating = false;
		extending = true;
	}
}
