using UnityEngine;

namespace Script.UI.MiniGame
{
    public class DDOSMiniGame : MonoBehaviour
    {
        public GameObject zombiePrefab;
        public float zombieGenerateCd;
        
        private Animator _portAnimator;
        public static DDOSMiniGame Instance { get; private set; }

        private void Start()
        {
            Instance = this;
            _portAnimator = transform.Find("Port").GetComponent<Animator>();
        }

        public void Init()
        {
            
        }
        
    }
}