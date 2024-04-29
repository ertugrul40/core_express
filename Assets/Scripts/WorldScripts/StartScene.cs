using UnityEngine;

namespace Gokboerue.Gameplay
{
    public class StartScene : MonoBehaviour
    {
        public MapGenerator mapGenerator;

        private void Start()
        {
            mapGenerator.GenerateMap();
        }
    }
}
