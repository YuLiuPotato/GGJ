using Script.Icon;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class InteractiveBtn : MonoBehaviour
    {
        public Text text;
        public SpriteRenderer image;
        public Sprite imageOri, imagePre;
        public bool isInUse;

        public InteractiveIcon usingIcon;
        
        private Animator _animator;

        public static InteractiveBtn Instance { get; private set; }

        private void Start()
        {
            Instance = this;
            _animator = GetComponent<Animator>();
        }
        
        public void Appear(InteractiveIcon icon)
        {
            usingIcon = icon;
            image.sprite = imageOri;
            image.enabled = true;
            text.enabled = true;
            text.text = icon.displayText;
            isInUse = true;
            _animator.SetTrigger(AnimTriggerHash.Appear);
        }
        
        public void Disappear(bool isPressed = false)
        {
            if (isPressed) image.sprite = imagePre;
            usingIcon = null;
            isInUse = false;
            _animator.SetTrigger(AnimTriggerHash.Disappear);
        }

        private void Update()
        {
            if (!isInUse) return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                usingIcon.Activate();
                Disappear(true);
            }
        }

        // 由动画器调用
        private void DisableDisplay()
        {
            image.enabled = false;
            text.enabled = false;
        }
    }
}