using UnityEditor;
using UnityEngine;


namespace Script.UI
{
    
    public class EmailUI : MonoBehaviour
    {
        public Transform playerSpawnPoint2;
        // public Transform icons;
        
        private Animator _animator;

        public void Init()
        {
            _animator = GetComponent<Animator>();
            gameObject.SetActive(false);
        }

        public void AppearEmail()
        {
            _animator.SetTrigger(AnimTriggerHash.Appear);
            UIManager.Instance.player.transform.position = playerSpawnPoint2.position;

            // // icon下落效果
            // for (var i = 0; i < icons.childCount; i++)
            // {
            //     var child = icons.GetChild(i);
            //     var pos = child.position;
            //     pos.y += 0.3f;
            //     child.position = pos;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            UIManager.Instance.CloseEmailUI();
        }
    }
}