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
        [SerializeField] private TextMeshProUGUI oreX;
        [SerializeField] private TextMeshProUGUI oreY;
        [SerializeField] private TextMeshProUGUI oreZ;
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
                    depth = Mathf.Abs(playerObject.transform.position.y);
                }
                if (depth < -73f)
                {
                    Debug.Log("Chapter 2");
                }
                if(depth < -150f)
                {
                    Debug.Log("Chapter 2");
                }
            }
        }

        public void CheckChapterEligibility()
        {
            var playerController = playerObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if (this.currentChapter == 2 && !playerController.chapter1Done)
                {
                    Debug.Log("Destroy when Chapter 2");
                }
                else if (this.currentChapter == 3 && !playerController.chapter2Done)
                {
                    Debug.Log("Destroy when Chapter 3");
                }
                else if (this.currentChapter == 4 && !playerController.chapter3Done)
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
