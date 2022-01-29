using UnityEngine;

namespace Script
{
    public class PlayerMovement : MonoBehaviour
    {
        public float force;

        private Rigidbody player;
        private GameObject imageObj;

        private Animator player_animator;
        // Start is called before the first frame update
        void Start()
        {
            force = 3.5f;
            player = GetComponent<Rigidbody>();
            imageObj = transform.Find("PlayerImage").gameObject;
            player_animator = imageObj.GetComponent<Animator>();
        }
        
        // Update is called once per frame
        void Update()
        {
            var velocityY = player.velocity.y;
            Vector3 velocity = new Vector3(Input.GetAxis("Horizontal")*force,velocityY,Input.GetAxis("Vertical")*force);
            player.velocity = velocity;
            if (player.velocity.x > 0.01)
            {   imageObj.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                player_animator.SetTrigger(AnimTriggerHash.Walk); 
            }
            else if (player.velocity.x < -0.01)
            {
                imageObj.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
                player_animator.SetTrigger(AnimTriggerHash.Walk); 
            
            }
            else
            {
                player_animator.SetTrigger(AnimTriggerHash.Idle);
            }


        }
    }
}
