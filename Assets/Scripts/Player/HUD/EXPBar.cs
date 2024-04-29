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

        public void ResetExp()
        {
            currentEXP = 0;
            UpdateText();
            UpdateExpBar();
        }
            
        public void AddEXP(float exp)
        {
            currentEXP += exp;
            UpdateText();
            UpdateExpBar();

            if(currentEXP >= 100)
            {
                currentEXP = 0;

                // TODO: Level Up
            }
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
