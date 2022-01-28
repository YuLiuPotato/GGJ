using System;
using UnityEngine;

namespace Script.Checker
{
    public class HoldableObjChecker : MonoBehaviour
    {
        public Transform holdingPos;
        public HoldableObject blockingIcon;
        public bool isHolding;
        public bool nearPool;

        private void Start()
        {
            holdingPos = transform.Find("HoldingPos").transform;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Holdable")) return;
            
            blockingIcon = other.gameObject.GetComponent<HoldableObject>();
            blockingIcon.Select();
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.CompareTag("Holdable")) return;
            
            var script = other.gameObject.GetComponent<HoldableObject>();
            blockingIcon = null;
            script.Release();
        }
    }
}