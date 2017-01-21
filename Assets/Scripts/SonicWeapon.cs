using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWeapon : MonoBehaviour {

	public int damage = 1;
	public float maxSize = 2.0f;
	public float shotSpeed;
	public bool staticLength = false;
	public Color baseColor = Color.red;
	public AudioClip staticClip, expandingClip;
	public float beamResetOffset = 0.1f;

	private bool animating = false, extending = true;
	private AudioSource audioSource;
	private SonicCollider sonicCollider;
	private Player myPlayer;
	private Enemy myEnemy;
	private float nextBeamReset;

	void Start () {
		foreach(Transform child in transform) {
			child.localScale = new Vector3 (0.0f, 0.3f, 1.0f);
		}
		foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()) {
			renderer.color = baseColor;
		}
		audioSource = GetComponent<AudioSource>();
		sonicCollider = GetComponentInChildren<SonicCollider>();
		myPlayer = GetComponentInParent<Player>();
		myEnemy = GetComponentInParent<Enemy>();
	}

	void Update () {
		if(animating) {
			if(staticLength) {
				if(Time.time > nextBeamReset) {
					sonicCollider.StartDetecting();
					nextBeamReset = Time.time + beamResetOffset;
				}
			}
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
		if(sonicCollider) {
			sonicCollider.StartDetecting();
			nextBeamReset = Time.time + beamResetOffset;
		}
	}

	public void StopFiring() {
		if(!staticLength && animating) return;
		if(audioSource) audioSource.Stop();
		foreach(Transform child in transform) {
			child.localScale = new Vector3 (0.0f, 0.3f, 1.0f);
		}
		animating = false;
		extending = true;
		if(sonicCollider) sonicCollider.StopDetecting();
	}

	public void HitPlayer(Player player) {
		player.TakeDamage(damage);
	}

	public void HitEnemy(Enemy enemy) {
		enemy.Damage(damage);
	}
}
