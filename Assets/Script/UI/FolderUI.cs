using UnityEditor;
using UnityEngine;

namespace Script.UI
{
    public class FolderUI : MonoBehaviour
    {
        public Transform playerSpawnPoint;
        public Transform icons;
        
        private Animator _animator;

        public void Init()
        {
            _animator = GetComponent<Animator>();
            gameObject.SetActive(false);
        }

        public void Appear()
        {
            _animator.SetTrigger(AnimTriggerHash.Appear);
            UIManager.Instance.player.transform.position = playerSpawnPoint.position;
            
            // icon下落效果
            for (var i = 0; i < icons.childCount; i++)
            {
                var child = icons.GetChild(i);
                var pos = child.position;
                pos.y += 0.3f;
                child.position = pos;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            UIManager.Instance.CloseFolderUI();
        }
    }
}