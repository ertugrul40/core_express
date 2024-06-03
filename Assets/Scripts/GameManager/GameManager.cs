using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gokboerue.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject playerObject;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject joyStick;
        [SerializeField] public TextMeshProUGUI oreX;
        [SerializeField] public  TextMeshProUGUI oreY;
        [SerializeField] public  TextMeshProUGUI oreZ;
        public bool chapter1Done = false;
        public bool chapter2Done = false;
        public bool chapter3Done = false;

        private int currentChapter = 1;


        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            // Check if instance already exists and destroy this if so
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Set the instance to this script
            Instance = this;

            // Make sure this object persists across scenes
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateChapter();
        }

        public void UpdateChapter()
        {
            if (playerObject != null)
            {
                float depth = 0;
                if (playerObject.transform.position.y < 1)
                {
                    depth = playerObject.transform.position.y;
                    if (depth < -73f)
                    {
                        Debug.Log("Chapter 2");
                        CheckChapterEligibility();
                    }
                    if (depth < -150f)
                    {
                        Debug.Log("Chapter 2");
                        CheckChapterEligibility();
                    }
                }
               
            }
        }

        public void CheckChapterEligibility()
        {
            var playerController = playerObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (this.currentChapter == 2 && !chapter1Done)
                {
                    Debug.Log("Destroy when Chapter 2");
                }
                else if (this.currentChapter == 3 && !chapter2Done)
                {
                    Debug.Log("Destroy when Chapter 3");
                }
                else if (this.currentChapter == 4 && !chapter3Done)
                {
                    Debug.Log("Destroy when Chapter 4");
                }
            }
        }

        public void GameOver()
        {


            joyStick.SetActive(false);
            //Destroy(playerObject);
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over");
        }

        public void RestartGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
        }
    }
}
