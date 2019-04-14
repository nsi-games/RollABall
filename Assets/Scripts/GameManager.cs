using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance = null;
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    public int score = 0;
    public TextMeshProUGUI scoreText;

    public void GameOver()
    {
        print("Game is over!!");
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }


    #region Extras
    public void NextLevel()
    {
        // Get current scene
        Scene current = SceneManager.GetActiveScene();
        // Load next scene
        SceneManager.LoadScene(current.buildIndex + 1);
    }
    #endregion
}