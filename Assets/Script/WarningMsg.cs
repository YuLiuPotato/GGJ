using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class WarningMsg : MonoBehaviour
    {
        private Text text;
        private Animator _animator;
        
        public static WarningMsg Instance { get; private set; }

        private void Start()
        {
            Instance = this;
            text = transform.Find("msg").GetComponent<Text>();
            _animator = GetComponent<Animator>();
        }

        public void ShowMsg(string msg)
        {
            text.text = "< " + msg + " >";
            _animator.SetTrigger(AnimTriggerHash.Appear);
        }
    }
}