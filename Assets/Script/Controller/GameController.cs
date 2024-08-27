using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;

        private int _score = 0;
        private bool _isGameOver = false;
        private void Awake()
        {
            if (_instance)
            {
                Destroy(_instance);
            }
            else
            {
                _instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        public static GameController Instance => _instance;

        public int Score
        {
            get => _score;
            set => _score = value;
        }

        public bool IsGameOver
        {
            get => _isGameOver;
            set => _isGameOver = value;
        }
        
        public void AddScore(int score)
        {
            _score += score;
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ContinueGame()
        {
            Time.timeScale = 1;
        }
    }
}