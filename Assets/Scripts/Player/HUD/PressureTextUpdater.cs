using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

namespace Gokboerue.Gameplay
{
    public class PressureTextUpdater: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pressureText;
        [SerializeField] private Transform targetObject; // The GameObject whose Y position we're tracking
        [SerializeField] private float pressureFactor = 10f; // Factor to convert Y position to pressure

        void Update()
        {
            UpdatePressureText();
        }

        void UpdatePressureText()
        {
            if (targetObject != null)
            {
                float depth = 0;
                if (targetObject.position.y < 1)
                {
                     depth = Mathf.Abs(targetObject.position.y);
                }

                float pressure = CalculatePressure(depth);
                pressureText.text = "Pressure: " + pressure.ToString("F1") + "P";

            }
        }

        float CalculatePressure(float depth)
        {
            return depth / 20f; // 1 Pascal for every 10 units of depth
        }
    }
}
