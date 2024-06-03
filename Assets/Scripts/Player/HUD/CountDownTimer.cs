using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace Gokboerue.Gameplay
{
    public class CountDownTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText; // Reference to the TextMeshProUGUI component
        [SerializeField] private float countdownTime = 420f; // Total countdown time in seconds (7 minutes)


        private float currentTime;

        void Start()
        {
            currentTime = countdownTime;
            StartCoroutine(StartCountdown());
        }

        IEnumerator StartCountdown()
        {
            while (currentTime > 0)
            {
                UpdateCountdownText();
                yield return new WaitForSeconds(1f); // Wait for 1 second
                currentTime--;
            }

            currentTime = 0;
            UpdateCountdownText(); // Ensure the countdown ends at 0
            OnCountdownEnd(); // Call a method when the countdown ends
        }

        void UpdateCountdownText()
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            countdownText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }

        void OnCountdownEnd()
        {
            GameManager.Instance.GameOver();
        }
    }
}
