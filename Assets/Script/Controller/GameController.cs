using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Slider healSlider;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject losePane;
        [SerializeField] private GameObject winPane;
        [SerializeField] private Player _player;
        
        private int _score = 0;
        private bool _isGameOver = false;
        
        private static GameController _instance;
        public static GameController Instance {
            get {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameController>();
                    if (_instance == null) {
                        GameObject obj = new GameObject ();
                        _instance = obj.AddComponent<GameController>();
                    }
                }
                return _instance;
            }
        }
        void Awake ()
        {
            if (_instance != null) Destroy(_instance.gameObject);
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            scoreText.text = "Score: " + _score;
            QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            healSlider.value = _player.Heal;
            if (_player.Heal <= 0) _isGameOver = true;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.P))
            {
                Debug.Log(Math.Abs(Time.timeScale));
                if (Math.Abs(Time.timeScale) > 0.5f) PauseGame();
                else ContinueGame();
            }

            if (Input.GetKey(KeyCode.N))
            {
                Reset();
            }
            if (_isGameOver)
            {
                losePane.gameObject.SetActive(true);
            }
        }

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
            scoreText.text = "Score: " + _score;
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            pausePanel.gameObject.SetActive(true);
        }

        public void ContinueGame()
        {
            Time.timeScale = 1;
            pausePanel.gameObject.SetActive(false);
        }

        public void Reset()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GamePlay");
        }

        public void ChangeMenu()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}