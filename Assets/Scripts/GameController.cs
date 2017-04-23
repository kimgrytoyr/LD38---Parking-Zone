using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject gameOverPanel;
	public GameObject mainMenuPanel;
	public GameObject wonPanel;

	public Text timeText;
	public Text highscoreText;
	public GameObject car;

	float startTime;
	float highscore;

	void Start() {
		Time.timeScale = 1;
		string highscoreString = "-";
		if (PlayerPrefs.HasKey("highscore")) {
			highscore = PlayerPrefs.GetFloat ("highscore");
			highscoreString = GetTimeString(new System.TimeSpan (0, 0, (int)highscore));
		}
		highscoreText.text = "Highscore: " + highscoreString;

		startTime = Time.time;
	}

	public void Parked() {
		System.TimeSpan ts = new System.TimeSpan (0, 0, (int)(Time.time - startTime));
		if (ts.TotalSeconds < highscore) {
			PlayerPrefs.SetFloat ("highscore", (float)ts.TotalSeconds);
		}

		wonPanel.SetActive (true);
		car.GetComponent<AudioSource> ().Stop ();
		Time.timeScale = 0;
	}

	void Update () {
		if (Input.GetKeyDown ("escape")) {
			if (mainMenuPanel.activeSelf) {
				ContinueGame ();
			} else {
				mainMenuPanel.SetActive (true);
				Time.timeScale = 0;
			}
		}

		System.TimeSpan ts = new System.TimeSpan (0, 0, (int)(Time.time - startTime));
		timeText.text = "Time: " + GetTimeString (ts);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("Player")) {
			gameOverPanel.SetActive (true);

			GetComponent<AudioSource> ().Play ();

			Destroy (other.gameObject);
		}
	}

	public void ContinueGame() {
		mainMenuPanel.SetActive (false);
		Time.timeScale = 1;
	}

	public void RestartGame() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void QuitGame() {
		Application.Quit ();
	}

	public void MainMenu() {
		SceneManager.LoadScene ("Start");
	}

	string GetTimeString(System.TimeSpan ts) {
		return ts.Minutes.ToString ("D2") + ":" + ts.Seconds.ToString ("D2");
	}
}
