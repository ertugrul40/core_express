using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gokboerue.Gameplay
{
    public class Drill : MonoBehaviour
    {
        [SerializeField] public float miningDuration = 0.05f;
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private Joystick joystick;
        [SerializeField] private GameObject mineParticle;
        [SerializeField] private EXPBar expBar;

        private Vector2 previousDirection;
        private float currentMiningDuration;
        public bool isMining;

        private void Start()
        {
            currentMiningDuration = miningDuration;
        }

        private void Update()
        {
            if(isMining)
            {
                mineParticle.SetActive(true);
            }
            else
            {
                mineParticle.SetActive(false);
            }

            if (joystick.Direction != Vector2.zero)
            {
                Vector3 direction = new Vector3(joystick.Direction.x, joystick.Direction.y, 0);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                previousDirection = direction;

                if(groundCheck.isGrounded)
                {
                    isMining = true;
                }
                else
                {
                    isMining = false;
                }

                currentMiningDuration -= Time.deltaTime;

                if (currentMiningDuration <= 0)
                {
                    Mine(direction);
                    currentMiningDuration = 0.05f;
                }
            }
            else
            {
                isMining = false;
                currentMiningDuration = 0.05f;

                float angle = Mathf.Atan2(previousDirection.y, previousDirection.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

        private void Mine(Vector3 direction)
        {
            try
            {
                var childPos = transform.GetChild(0).position;
                RaycastHit2D hit = Physics2D.Raycast(childPos, direction, 0.1f);
                if (hit.collider != null)
                {
                    if(hit.collider.GetComponent<Tilemap>() != null)
                    {
                        hit.collider.GetComponent<Tilemap>().SetTile(hit.collider.GetComponent<Tilemap>().WorldToCell(hit.point), null);
                        expBar.AddEXP(5);
                    }
                   
                }
                else
                {
                    currentMiningDuration = miningDuration;
                }
            }
            catch (System.Exception ex)
            {

              
            }
            
        }


    }
}