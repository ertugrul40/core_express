using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gokboerue.Gameplay
{
    public class EXPBar : MonoBehaviour
    {
        [SerializeField] private Slider expBarSlider;
        [SerializeField] private TextMeshProUGUI expBarText;

        public float currentEXP = 0;

        [SerializeField] Drill drill;

        public void ResetExp()
        {
            currentEXP = 0;
            UpdateText();
            UpdateExpBar();
        }
            
        public void AddEXP(float exp)
        {

            if (currentEXP >= 100)
            {
                StartCoroutine(ChangeBool());
            }
            else
            {
                currentEXP += exp;
                UpdateText();
                UpdateExpBar();
            }
        }

        IEnumerator ChangeBool()
        {
            // Change the mining to false
            // Check if drill is assigned
            if (drill == null)
            {
                Debug.LogError("Drill is not assigned.");
                yield break;
            }

            Debug.Log("ChangeBool coroutine started");

            // Change the mining to false
            drill.miningDuration = 10f;
            Debug.Log("drill.isMining set to false");

            // Wait for 2 seconds
            yield return new WaitForSeconds(2);

            currentEXP = 0;

            // Change the mining back to true
            drill.miningDuration = 0.1f;
            Debug.Log("drill.isMining set to true");

            UpdateText();
            UpdateExpBar();
        }

        private void UpdateText()
        {
            expBarText.text = currentEXP.ToString() + "%";
        }

        private void UpdateExpBar()
        {
            expBarSlider.value = currentEXP;
        }
    }
}
