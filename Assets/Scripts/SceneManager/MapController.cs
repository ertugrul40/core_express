using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gokboerue.Gameplay
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private GameObject mapPanel; // Reference to the map panel
        [SerializeField] private Button mapButton; // Reference to the map button
        [SerializeField] private Button closeButton; // Reference to the close button

        void Start()
        {
            // Ensure the map is initially hidden
            mapPanel.SetActive(false);

            // Add listeners to the buttons
            mapButton.onClick.AddListener(OpenMap);
            closeButton.onClick.AddListener(CloseMap);
        }

        void OpenMap()
        {
            // Show the map panel
            mapPanel.SetActive(true);
        }

        void CloseMap()
        {
            // Hide the map panel
            mapPanel.SetActive(false);
        }
    }
}
