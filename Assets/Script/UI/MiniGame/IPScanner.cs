using UnityEngine;

namespace Script.UI.MiniGame
{
    public class IPScanner : MonoBehaviour
    {
        public Animator scannerRange;
        public float scanCd;  // 扫描间隔
        
        private float _nextScanTime;
        private bool _isScanning;
        
        public void Execute()
        {
            _isScanning = true;
            _nextScanTime = Time.time + scanCd;
        }

        public void Kill()
        {
            _isScanning = false;
        }

        private void Update()
        {
            if (!_isScanning) return;
            if (Time.time > _nextScanTime)
            {
                scannerRange.SetTrigger(AnimTriggerHash.Appear);
                _nextScanTime = Time.time + scanCd;
            }
        }

    }
}