using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public const string WRAPPER = "WrapperObject";
	public const string RESUME_BUTTON = "ResumeButton";

	private Menu menu;
	private UserInterface userInterface;
	private Player player;

	private bool launched = false;

	void Start () {
		player = Component.FindObjectOfType<Player>();
		menu = Component.FindObjectOfType<Menu>();
		userInterface = Component.FindObjectOfType<UserInterface>();
		if(userInterface) userInterface.Hide();
	}

	void Update () {
		if(!launched) {
			launched = true;
			LaunchGame();
		}
		if(Input.GetKeyDown(KeyCode.Escape)) {
			PauseGame();
			return;
		}
		userInterface.Refresh(player);
	}

	public void LaunchGame() {
		if(menu) menu.Hide();
		if(userInterface) {
			userInterface.Show();
			userInterface.Refresh(player);
		}
		Cursor.visible = false;
		/**
		 * TODO: start the actual game
		 */
	}

	public void PauseGame() {
		Time.timeScale = 0.0f;
		if(menu) menu.ShowPause();
		if(userInterface) userInterface.Hide();
		Cursor.visible = true;
	}

	public void ResumeGame() {
		Time.timeScale = 1.0f;
		if(menu) menu.Hide();
		if(userInterface) userInterface.Show();
		Cursor.visible = false;
	}

	public void GameOver(bool success) {
		Debug.Log("GAME OVER: " + success);
		//show relevant screen
	}

	public void ExitGame() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif
		Application.Quit();
		#endif
	}
}
