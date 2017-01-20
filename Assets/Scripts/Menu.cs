using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	private GameManager manager;
	private GameObject wrapper;
	private Text resumeButtonText;
	private bool paused = false;

	void Start () {
		manager = Component.FindObjectOfType<GameManager>();
		foreach(Transform child in transform) {
			if(child.CompareTag(GameManager.WRAPPER)) {
				wrapper = child.gameObject;
				foreach(Transform grandchild in child) {
					if(grandchild.CompareTag(GameManager.RESUME_BUTTON)) {
						resumeButtonText = grandchild.gameObject.GetComponentInChildren<Text>();
						break;
					}
				}
				break;
			}
		}
	}

	void Update () {
		
	}

	public void NewGameClicked() {
		if(manager) {
			if(paused) manager.ResumeGame();
			else manager.LaunchGame();
		}
	}

	public void ExitClicked() {
		if(manager) manager.ExitGame();
	}

	public void Show() {
		wrapper.SetActive(true);
	}

	public void ShowPause() {
		Show();
		resumeButtonText.text = "Resume";
		paused = true;
	}

	public void Hide() {
		wrapper.SetActive(false);
	}
}
