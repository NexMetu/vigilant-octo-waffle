using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

	private GameObject wrapper;
	private Text armour, ammo, energy, health;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform) {
			if(child.CompareTag(GameManager.WRAPPER)) {
				wrapper = child.gameObject;
				break;
			}
		}
		GetComponentsInChildren<Text>();
		foreach(Text textElement in GetComponentsInChildren<Text>()) {
			if(textElement.name == "Armour") armour = textElement;
			else if(textElement.name == "Ammo") ammo = textElement;
			else if(textElement.name == "Energy") energy = textElement;
			else if(textElement.name == "Health") health = textElement;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Show() {
		wrapper.SetActive(true);
	}

	public void Hide() {
		wrapper.SetActive(false);
	}

	public void Refresh(Player player) {
		if(player == null) return;
		if(armour) armour.text = player.armour + "";
		if(ammo) ammo.text = player.ammo + "";
		if(energy) energy.text = player.energy + "";
		if(health) health.text = player.health + "";
	}
}
