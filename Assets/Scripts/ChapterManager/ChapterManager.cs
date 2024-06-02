using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gokboerue.Gameplay
{
    public class ChapterManager : MonoBehaviour
    {
        [SerializeField] private Transform targetObject;
        [SerializeField] private TextMeshProUGUI oreX;
        [SerializeField] private TextMeshProUGUI oreY;
        [SerializeField] private TextMeshProUGUI oreZ;
        private int currentChapter = 1;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            UpdateChapter();
        }

        void UpdateChapter()
        {
            if (targetObject != null)
            {
                float depth = 0;
                if (targetObject.position.y < 1)
                {
                    depth = Mathf.Abs(targetObject.position.y);
                }
                if(depth < -73f)
                {
                    Debug.Log("Chapter 2");
                }
                //float pressure = CalculatePressure(depth);
                //pressureText.text = "Pressure: " + pressure.ToString("F1") + "P";

            }
        }
    }
}
