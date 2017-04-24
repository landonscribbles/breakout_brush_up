using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

    public int lives = 3;
    public int bricks = 20;
    public float resetDelay = 1f;
    public Text livesText;
    public GameObject gameOver;
    public GameObject youWon;
    public GameObject bricksPrefab;
    public GameObject paddle;
    public GameObject deathParticles;
    public static GM instance = null;

    private GameObject clonePaddle;

	void Start () {
        Cursor.visible = false; 
		if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        Setup();
	}

    public void Setup() {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity);
        Instantiate(bricksPrefab, transform.position, Quaternion.identity);
    }

    void CheckGameOver() {
        if (bricks < 1) {
            youWon.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }

        if (lives < 1) {
            gameOver.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }
    }

    private void Reset() {
        Time.timeScale = 1f;
        // TODO: Update to SceneManager
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseLife() {
        lives--;
        livesText.text = "Lives: " + lives;
        Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
        Destroy(clonePaddle);
        Invoke("SetupPaddle", resetDelay);
        CheckGameOver();
    }

    void SetupPaddle() {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity);
    }

    public void DestroyBrick() {
        bricks--;
        CheckGameOver();
    }

    private bool CheckForEsc() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            return true;
        }
        return false;
    }

    private void Update() {
        if (CheckForEsc()) {
            Application.Quit();
            Cursor.visible = true;
        }
    }

}
