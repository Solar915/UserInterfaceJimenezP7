using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public TextMeshProUGUI livesText;
    private int lives;
    public GameObject pauseScreen;
    private bool paused;
    public bool isGameActive;
    private int score;
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);


        ;

    }

    // Update is called once per frame
    void Update()
    {
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
         
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }


    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);

        void UpdateLives(int livesToChange)
        {
            lives += livesToChange;
            livesText.text = "lives: " + lives;
            if (lives <= 0)
            {
                GameOver();
            }
        }

        void GameOver()
        {
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            isGameActive = false;
        }

        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void StartGame(int difficulty)
        {
            isGameActive = true;
            StartCoroutine(SpawnTarget());
            score = 0;
            spawnRate /= difficulty;
            UpdateScore(0);
            UpdateLives(3);

            titleScreen.gameObject.SetActive(false);
        }

        void ChangePaused()
        {
            if (!paused)
            {
                paused = true;
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                pauseScreen.SetActive(false);
                Time.timeScale = 1;

            }
        } 
    }
}
