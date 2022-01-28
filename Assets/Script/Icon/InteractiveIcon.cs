using UnityEngine;

namespace Script.Icon
{
    public abstract class InteractiveIcon : MonoBehaviour
    {
        public string displayText;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            if (!InteractiveBtn.Instance.isInUse) InteractiveBtn.Instance.Appear(this);
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            if (InteractiveBtn.Instance.usingIcon == this) InteractiveBtn.Instance.Disappear();
        }

        public abstract void Activate();
    }
}