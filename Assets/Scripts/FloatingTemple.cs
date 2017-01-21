using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTemple : MonoBehaviour {

	public float warningTime = 5.0f;
	public AudioClip warning, crushing;

	private bool triggerTrap = false, trapSprung = false;
	private float launchTrapTime;
	private AudioSource audioSource;
	private FadingAudioSource fadingAudioSource;
	private Player player;
	private GameManager manager;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		fadingAudioSource = GetComponent<FadingAudioSource>();
		player = GameObject.FindObjectOfType<Player>();
		manager = GameObject.FindObjectOfType<GameManager>();
	}

	void Update () {
		if(triggerTrap) {
			//TODO: detect when player leaves so trap is not sprung
			if(Time.time > launchTrapTime) {
				triggerTrap = false;
				trapSprung = true;
				StartCoroutine(SpringTrap());
			}
		}
	}

	private IEnumerator SpringTrap() {
//		if(audioSource) {
////			audioSource.Stop();
//			audioSource.clip = crushing;
//			audioSource.loop = false;
//			audioSource.Play();
//		}
		if(fadingAudioSource) {
			fadingAudioSource.Fade(crushing, 0.7f, false);
		}
		yield return new WaitForSeconds(1.0f);
		Rigidbody rigidbody = GetComponent<Rigidbody>();
		if(rigidbody) {
			rigidbody.useGravity = true;
			rigidbody.isKinematic = false;
		}
		BoxCollider collider = GetComponent<BoxCollider>();
		if(collider) {
			Vector3 size = collider.size;
			size.y = 2;
			collider.size = size;
		}
		if(player) {
			Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
			if(playerRigidbody) playerRigidbody.isKinematic = true;
			foreach(Collider playerCollider in player.GetComponentsInChildren<Collider>()) {
				playerCollider.enabled = false;
			}
			yield return new WaitForSeconds(2.0f);
			player.health = 0;
			manager.GameOver(false);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if(triggerTrap || trapSprung) return;
		Player player = collision.gameObject.GetComponent<Player>();
		if(player) {
			Debug.Log("collision: player under");
			triggerTrap = true;
			launchTrapTime = Time.time + warningTime;
			if(fadingAudioSource) {
				fadingAudioSource.Fade(warning, 0.7f, true);
			}
//			if(audioSource) {
//				audioSource.clip = warning;
//				audioSource.loop = true;
//				audioSource.Play();
//			}
		}
	}
}
