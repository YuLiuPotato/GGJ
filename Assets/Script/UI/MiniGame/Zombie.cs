using Pathfinding;
using UnityEngine;

namespace Script.UI.MiniGame
{
    public class Zombie : MonoBehaviour
    {
        private Animator _animator;
        public int kind;
        public GameObject ipTag;
        
        private SpriteRenderer _image;
        private AIDestinationSetter _aids;
        private AIPath _aiPath;
        // private RectTransform rt;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _aids = GetComponent<AIDestinationSetter>();
            _aiPath = GetComponent<AIPath>();
        }

        public void SetInfo(Transform target, int newKind, Color color)
        {
            kind = newKind;
            _image.color = color;
            _aids.target = target;
        }

        public void SetSpeed(float speed)
        {
            _aiPath.maxSpeed = speed;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            // ip scanner 检测到僵尸
            if (!other.CompareTag("IPScanRange")) return;
            Killed();
        }

        private void Killed()
        {
            var obj = Instantiate(ipTag, transform.position, Quaternion.identity);
            
            // 下落效果
            var pos = obj.transform.position;
            pos.y += 0.5f;
            obj.transform.position = pos;
            
            // 设置颜色
            // obj.transform.Find("image").GetComponent<SpriteRenderer>().color = _image.color;
            
            // Destroy(gameObject);
        }
    }
}