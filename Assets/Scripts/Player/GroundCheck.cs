using UnityEngine;

namespace Gokboerue.Gameplay
{
    public class GroundCheck : MonoBehaviour
    {
        public bool isGrounded;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Tilemap"))
            {
                isGrounded = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Tilemap"))
            {
                isGrounded = false;
            }
        }
    }
}
