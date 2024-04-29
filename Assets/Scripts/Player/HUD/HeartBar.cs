using UnityEngine;

namespace Gokboerue.Gameplay
{
    public class HeartBar : MonoBehaviour
    {
        [SerializeField] private GameObject heartSpritePrefab;
        [SerializeField] private GameObject halfHeartSpritePrefab;
        [SerializeField] private GameObject heartBarHorizontalLayout;

        [Range(0, 8)]
        public float heartCount;

        private void Start()
        {
            UpdateHeartBar();
        }

        public void UpdateHeartBar()
        {
            if(heartCount > 8)
            {
                heartCount = 8;
            }

            foreach (Transform child in heartBarHorizontalLayout.transform)
            {
                Destroy(child.gameObject);
            }

            if (heartCount % 1 == 0)
            {
                for (int i = 0; i < heartCount; i++)
                {
                    Instantiate(heartSpritePrefab, heartBarHorizontalLayout.transform);
                }
            }
            else
            {
                for (int i = 0; i < heartCount - 1; i++)
                {
                    Instantiate(heartSpritePrefab, heartBarHorizontalLayout.transform);
                }
                Instantiate(halfHeartSpritePrefab, heartBarHorizontalLayout.transform);
            }
        }
    }
}
