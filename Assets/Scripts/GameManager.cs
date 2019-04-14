using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall
{
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
        public bool isGameRunning = true;
        public Transform collectables;
        [Header("UI")]
        public Text scoreText;

        void Update()
        {
            // If the game is not running
            if (!isGameRunning)
                return; // Exit Update

            // Are all collectables gone?
            if(collectables.childCount == 0)
            {
                // Game is no longer running
                isGameRunning = false;
                // Game Over
                GameOver();
            }
        }

        void GameOver()
        {

        }

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
