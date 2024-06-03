using TMPro;
using UnityEngine;

namespace Gokboerue.Gameplay
{ 
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float fallThresholdVelocity = 5f;
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private Drill drill;
        [SerializeField] private Joystick joystick;
        public int OreX { get; set; } = 0;
        public int OreY { get; set; } = 0;
        public int OreZ { get; set; } = 0;


        private bool canTakeFallDamage = true;
        private float currentFallDamageTimer = 1f;
        private Rigidbody2D rb;
        private HeartBar heartBar;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            heartBar = GetComponent<HeartBar>();
        }

        private void FixedUpdate()
        {
            if (joystick.Direction != Vector2.zero)
            {
                Vector3 direction = new Vector3(joystick.Direction.x, joystick.Direction.y, 0);
                rb.velocity = direction.normalized * speed * Time.deltaTime;
                rb.angularVelocity = direction.magnitude;
            }
        }

        private void Update()
        {
            CheckFallDamage();

            if (!canTakeFallDamage)
            {
                currentFallDamageTimer -= Time.deltaTime;
                if (currentFallDamageTimer <= 0)
                {
                    canTakeFallDamage = true;
                    currentFallDamageTimer = 0.2f;
                }
            }
        }

        private void CheckFallDamage()
        {
            if (!groundCheck.isGrounded)
            {
                return;
            }

            if (drill.isMining)
            {
                return;
            }

            if (rb.velocity.y < -fallThresholdVelocity)
            {
                ApplyFallDamage();
            }
        }

        private void ApplyFallDamage()
        {
            if (canTakeFallDamage)
            {
                heartBar.heartCount -= .5f;
                heartBar.UpdateHeartBar();
                canTakeFallDamage = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Ore1_1")
            {
                Destroy(collision.gameObject, 0.5f);
                OreX += 1;
                GameManager.Instance.oreX.text = "OreX: " + OreX.ToString();
            }
            if (collision.tag == "Ore1_2")
            {
                Destroy(collision.gameObject, 0.5f);
                OreY += 1;
                GameManager.Instance.oreY.text = "OreY: " + OreX.ToString();
            }
            if (collision.tag == "Ore1_3")
            {
                Destroy(collision.gameObject, 0.5f);
                OreZ += 1;
                GameManager.Instance.oreZ.text = "OreZ: " + OreX.ToString();
            }
            if (collision.name == "ChapterLine1")
            {
                Destroy(collision.gameObject, 0.5f);
            }
            if(collision.name == "Chapter1Point")
            {
                if(OreX >= 3 && OreY >= 3 && OreZ >= 4)
                {
                    GameManager.Instance.chapter1Done = true;
                }
               
            }
        }
    }
}
